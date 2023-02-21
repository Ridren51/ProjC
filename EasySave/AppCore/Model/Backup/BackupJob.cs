using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Resources;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Xml.Linq;
using System.Threading;
using EasySave_CLI.Model.Logs;
using EasySave_CLI.Model;
using System.Text.Json;
using System.Text.Json.Serialization;
using AppCore.Model.Utilities;

namespace AppCore.Model.Backup
{
    public class BackupJob : IBackupJob
    {
        private readonly string _name;
        private readonly string _sourceDirectory;
        private readonly string _targetDirectory;
        private readonly List<string> _extensionToCrypt;
        private static Semaphore _heavyFile = new Semaphore(1,1);
        private double _heavyFileSize = Config.HeavyFileSize;
        private Thread _backupThread { get; set; }
        private ManualResetEvent _pauseEvent = new ManualResetEvent(true);

        public BackupEnum Type { get; set; }
        public BackupJob(string name, string sourceDirectory, string targetDirectory,
            BackupEnum type, ref List<string> extensionToCrypt, ref double heavyFileSize)
        {
            _name = name;
            _sourceDirectory = sourceDirectory;
            _targetDirectory = targetDirectory;
            Type = type;
            _extensionToCrypt = extensionToCrypt;
            _heavyFileSize=heavyFileSize;
        }
        public BackupJob(string name, string sourceDirectory, string targetDirectory, BackupEnum type)
        {
            _name = name;
            _sourceDirectory = sourceDirectory;
            _targetDirectory = targetDirectory;
            Type = type;
            _extensionToCrypt = new List<string>();
        }

        public override string ToString()
        {
            return _name;
        }
        public string ToJSON()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            return JsonSerializer.Serialize(new
            {
                this._name,
                this._sourceDirectory,
                this._targetDirectory,
                type = this.Type.ToString(),
            }, options);
        }
        public void StartBackup()
        {
            if(_backupThread == null || _backupThread.ThreadState == System.Threading.ThreadState.Stopped)
            {
                _backupThread = new Thread(DoBackup);
                _backupThread.Start();
            }
        }
        public void PauseBackup()
        {
            _pauseEvent.Reset();
        }

        public void ResumeBackup()
        {
            _pauseEvent.Set();
        }
        public async Task AsyncDoBackup()
        {
            await Task.Run(() =>
            {
                {
                    DirectoryInfo sourceDirectoryInfo = new DirectoryInfo(_sourceDirectory);
                    DirectoryInfo targetDirectoryInfo = new DirectoryInfo(_targetDirectory);
                    bool isComplete = Type == BackupEnum.Full;
                    foreach (FileInfo file in sourceDirectoryInfo.GetFiles())
                    {
                        if (isComplete && File.Exists(GetFileSourcePath(sourceDirectoryInfo, targetDirectoryInfo, file)))
                            CopyDirectory(_sourceDirectory, _targetDirectory, file.Name);
                        else
                        {
                            if (File.Exists(GetFileSourcePath(sourceDirectoryInfo, targetDirectoryInfo, file)) != File.Exists(GetFileTargetPath(sourceDirectoryInfo, targetDirectoryInfo, file)))
                                CopyDirectory(_sourceDirectory, _targetDirectory, file.Name);
                            else if (!CompareHash(sourceDirectoryInfo, targetDirectoryInfo, file))
                                CopyDirectory(_sourceDirectory, _targetDirectory, file.Name);
                        }
                    }
                }
            });
        }

        public void DoBackup()
        {
            DirectoryInfo sourceDirectoryInfo = new DirectoryInfo(_sourceDirectory);
            DirectoryInfo targetDirectoryInfo = new DirectoryInfo(_targetDirectory);
            bool isComplete = Type == BackupEnum.Full;
            foreach (FileInfo file in sourceDirectoryInfo.GetFiles())
            {
                bool isFileHeavy = file.Length >= _heavyFileSize;
                if (isFileHeavy)
                    _heavyFile.WaitOne();
                try
                {
                    _pauseEvent.WaitOne();
                    bool shouldCryptFile = _extensionToCrypt.Contains(file.Extension);
                    if (isComplete && File.Exists(GetFileSourcePath(sourceDirectoryInfo, targetDirectoryInfo, file)))
                        CopyDirectory(_sourceDirectory, _targetDirectory, file.Name, shouldCryptFile);
                    else
                    {
                        if (File.Exists(GetFileSourcePath(sourceDirectoryInfo, targetDirectoryInfo, file)) != File.Exists(GetFileTargetPath(sourceDirectoryInfo, targetDirectoryInfo, file)))
                            CopyDirectory(_sourceDirectory, _targetDirectory, file.Name, shouldCryptFile);
                        else if (!CompareHash(sourceDirectoryInfo, targetDirectoryInfo, file))
                            CopyDirectory(_sourceDirectory, _targetDirectory, file.Name, shouldCryptFile);
                    }
                }finally
                {
                    if (isFileHeavy)
                        _heavyFile.Release();
                }
            }
               
    }

        private int CryptFile(string inputPath, string outputPath)
        {
            string cryptoSoftPath = PathHandler.getRelativePath("Cryptosoft/CryptoSoft.exe");
            string arguments = $"{inputPath} {outputPath}";
            Console.WriteLine(arguments);
            Process process = Process.Start(cryptoSoftPath, arguments);
            process.WaitForExit(); // Wait for the process to finish running

            int exitCode = process.ExitCode; // Retrieve the exit code

            return exitCode;
        }

        private string GetFileSourcePath(DirectoryInfo sourceDirectoryInfo, DirectoryInfo targetDirectoryInfo, FileInfo file)
        {
            return Path.Combine(sourceDirectoryInfo.FullName, file.Name);
        }
        private string GetFileTargetPath(DirectoryInfo sourceDirectoryInfo, DirectoryInfo targetDirectoryInfo, FileInfo file)
        {
            return Path.Combine(targetDirectoryInfo.FullName, file.Name);
        }

        private static byte[] GetFileHash(string file, SHA256 sha256Hash)
        {
            using (FileStream stream = File.OpenRead(file))
            {
                return sha256Hash.ComputeHash(stream);
            }
        }

        private bool CompareHash(DirectoryInfo SourceDirectory, DirectoryInfo TargetDirectory, FileInfo file)
        {
            string sourceFilePath = GetFileSourcePath(SourceDirectory, TargetDirectory, file);
            string targetFilePath = GetFileTargetPath(SourceDirectory, TargetDirectory, file);

            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] sourceFileHash = GetFileHash(sourceFilePath, sha256Hash);
                byte[] targetFileHash = GetFileHash(targetFilePath, sha256Hash);

                if (!sourceFileHash.SequenceEqual(targetFileHash))
                    return false;
            }
            return true;
        }

        private void CopyDirectory(string SourceDirectory, string TargetDirectory, string Name, bool crypt = false)
        {
            DirectoryInfo source = new DirectoryInfo(SourceDirectory);
            DirectoryInfo target = new DirectoryInfo(TargetDirectory);

            long totalSize = 0;
            foreach (FileInfo file in source.GetFiles(Name))
            {
                totalSize += file.Length;
            }
            FileInfo[] directoryFiles = source.GetFiles();
            RealTimeLog realtimeLog = LogManager.GetNewRealTimeLog(totalSize, directoryFiles.Length);



            if (target.Exists == false)
            {
                target.Create();
            }
            foreach (FileInfo file in source.GetFiles(Name))
            {
                int cryptingTime = -1;
                var stopwatch = Stopwatch.StartNew();
                string fileTargetPath = Path.Combine(target.FullName, file.Name);
                Name = file.Name;
                DateTime Date = DateTime.Now;
                if (crypt)
                    cryptingTime = CryptFile(file.FullName, fileTargetPath);
                else
                    file.CopyTo(fileTargetPath, true);
                stopwatch.Stop();
                LogManager.UpdateRealTimeLog(realtimeLog, new TransferFile(_name, file.FullName, fileTargetPath, file.Length, stopwatch.Elapsed.Milliseconds, crypt ? cryptingTime : null));
                Console.WriteLine("- " + Name + " : " + Date.ToString() + " - " + +stopwatch.Elapsed.Seconds + " seconds");
            }

        }
    }
}


