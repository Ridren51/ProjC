using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Resources;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using EasySave_CLI.Model;

namespace EasySave_CLI.Model
{
    internal class BackupJob
    {
        private string _name;
        string Type { get; set; }

        public BackupJob(string name, string type)
        {
            _name = name;
            Type = type;
        }
        public void doJob(string SourceDirectory, string TargetDirectory)
        {
            CompareFiles(SourceDirectory, TargetDirectory);
            CopyDirectory(SourceDirectory, TargetDirectory, this._name);
        }
                 /*
                Console.WriteLine("Do you want to make a backup? (Y/N)");

                Console.WriteLine("Enter the path of the folder to be backed up:");

                Console.WriteLine("The specified folder does not exist.");
 
                Console.WriteLine("Enter the path of the backup folder:");
                */

        private string GetFileSourcePath(DirectoryInfo sourceDirectoryInfo, DirectoryInfo targetDirectoryInfo, FileInfo file)
        {
            return Path.Combine(sourceDirectoryInfo.FullName, file.Name);
        }
        private string GetFileTargetPath(DirectoryInfo sourceDirectoryInfo, DirectoryInfo targetDirectoryInfo, FileInfo file)
        { 
            return Path.Combine(targetDirectoryInfo.FullName, file.Name);
        }

        private void CompareFiles(string SourceDirectory, string TargetDirectory)
        {
            DirectoryInfo sourceDirectoryInfo = new DirectoryInfo(SourceDirectory);
            DirectoryInfo targetDirectoryInfo = new DirectoryInfo(TargetDirectory);

            foreach (FileInfo file in sourceDirectoryInfo.GetFiles())
            {

                if (File.Exists(GetFileTargetPath(sourceDirectoryInfo, targetDirectoryInfo, file)) == File.Exists(GetFileSourcePath(sourceDirectoryInfo, targetDirectoryInfo, file)))
                    continue;

                /*else if (System.IO.File.Exists(target.FullName) == System.IO.File.Exists(source.FullName) && System.IO.File.ReadAllBytes(TargetFile) != System.IO.File.ReadAllBytes(SourceFile))
                {
                    var stopwatch = Stopwatch.StartNew();
                    Console.WriteLine("The file " + Name + " is updating");
                    System.IO.File.Delete(TargetFile);
                    System.IO.File.Copy(SourceFile, TargetFile, true);
                    stopwatch.Stop();
                }*/
                else if (File.Exists(GetFileTargetPath(sourceDirectoryInfo, targetDirectoryInfo, file)) != File.Exists(GetFileSourcePath(sourceDirectoryInfo, targetDirectoryInfo, file)))
                    CopyDirectory(SourceDirectory, TargetDirectory, file.Name);
                else
                    Console.WriteLine("Error");
            }
        }

        private bool CompareHash(string SourceDirectory, string TargetDirectory)
        {
            DirectoryInfo sourceDirectoryInfo = new DirectoryInfo(SourceDirectory);
            DirectoryInfo targetDirectoryInfo = new DirectoryInfo(TargetDirectory);



            if (1 == 1)
                return true;
            else
                return false;

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
                file.CopyTo(Path.Combine(target.FullName, file.Name));
                Name = file.Name;
                DateTime Date = DateTime.Now;
                stopwatch.Stop();
                Console.WriteLine("- " + Name + " : " + Date.ToString() + " - " + +stopwatch.Elapsed.Seconds + " seconds");
            }

        }
    }
}


