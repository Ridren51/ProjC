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

namespace AppCore.Model.Backup
{
    public class BackupJob : IBackupJob
    {
        private string _name;
        private string _sourceDirectory;
        private string _targetDirectory;
        public BackupEnum Type { get; set; }
        public BackupJob(string name, string sourceDirectory, string targetDirectory, BackupEnum type)
        {
            _name = name;
            _sourceDirectory = sourceDirectory;
            _targetDirectory = targetDirectory;
            Type = type;
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
                IgnoreNullValues = true,
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
        public async Task DoBackup()
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

        private void CopyDirectory(string SourceDirectory, string TargetDirectory, string Name)
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
                var stopwatch = Stopwatch.StartNew();
                string fileSourcePath = Path.Combine(file.Directory.ToString(), file.Name);
                string fileTargetPath = Path.Combine(target.FullName, file.Name);
                file.CopyTo(fileTargetPath, true);
                Name = file.Name;
                DateTime Date = DateTime.Now;
                stopwatch.Stop();
                realtimeLog.UpdateLog(new TransferFile(_name, fileSourcePath, fileTargetPath, file.Length, stopwatch.Elapsed.Milliseconds));
                Console.WriteLine("- " + Name + " : " + Date.ToString() + " - " + +stopwatch.Elapsed.Seconds + " seconds");
            }

        }
    }
}


