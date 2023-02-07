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
using EasySave_CLI.Model;
using System.Timers;
using System.Threading;

namespace EasySave_CLI.Model
{
    internal class BackupJob : IBackupJob
    {
        private string _name;
        static System.Timers.Timer timer;
        string Type { get; set; }

        public BackupJob(string name, string type)
        {
            _name = name;
           Type = type;
        }

        
        public void SetTime(int interval)
        {
            timer = new System.Timers.Timer(interval);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }
        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            
        }

        public async void doDifferiencialBackup(string SourceDirectory, string TargetDirectory)
        {
            await Task.Run(() =>
            {

            });
        }
        public async void doBackup(string SourceDirectory,string TargetDirectory)
        {
            await Task.Run(() =>
            {
                {
                    DirectoryInfo sourceDirectoryInfo = new DirectoryInfo(SourceDirectory);
                    DirectoryInfo targetDirectoryInfo = new DirectoryInfo(TargetDirectory);

                    foreach (FileInfo file in sourceDirectoryInfo.GetFiles())
                    {

                        if (File.Exists(GetFileTargetPath(sourceDirectoryInfo, targetDirectoryInfo, file)) == File.Exists(GetFileSourcePath(sourceDirectoryInfo, targetDirectoryInfo, file)))
                        {
                            if (!CompareHash(sourceDirectoryInfo, targetDirectoryInfo, file))
                            {
                                CopyDirectory(SourceDirectory, TargetDirectory, file.Name);
                            }
                        }
                        else if (File.Exists(GetFileTargetPath(sourceDirectoryInfo, targetDirectoryInfo, file)) != File.Exists(GetFileSourcePath(sourceDirectoryInfo, targetDirectoryInfo, file)))
                            CopyDirectory(SourceDirectory, TargetDirectory, file.Name);
                            Console.WriteLine("pfeffsv");
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

            if (target.Exists == false)
            {
                target.Create();
            }

            foreach (FileInfo file in source.GetFiles(Name))
            {
                var stopwatch = Stopwatch.StartNew();
                file.CopyTo(Path.Combine(target.FullName, file.Name),true);
                Name = file.Name;
                DateTime Date = DateTime.Now;
                stopwatch.Stop();
                
                Console.WriteLine("- " + Name + " : " + Date.ToString() + " - " + +stopwatch.Elapsed.Seconds + " seconds");
            }

        }
    }
}


