using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Resources;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using EasySave_CLI.Model;

namespace EasySave_CLI.Model
{
    internal class BackupJob
    {
        string Name { get; set; }
        bool State { get; set; }
        string SourceDirectory { get; set; }
        string TargetDirectory { get; set; }
        string Type { get; set; }
        DateTime Date { get; set; }

        public void doBackupJob()
        {
            try
            {
                Console.WriteLine("Do you want to make a backup? (Y/N)");
                string answer = Console.ReadLine();

                if (answer.ToLower() != "y")
                {
                    return;
                }

                Console.WriteLine("Enter the path of the folder to be backed up:");
                string SourceDirectory = Console.ReadLine();
                DirectoryInfo source = new DirectoryInfo(SourceDirectory);

                if (source.Exists == false)
                {
                    Console.WriteLine("The specified folder does not exist.");
                    return;
                }

                Console.WriteLine("Enter the path of the backup folder:");
                string TargetDirectory = Console.ReadLine();
                DirectoryInfo target = new DirectoryInfo(TargetDirectory);

                if (target.Exists == false)
                {
                    target.Create();
                }

                CompareFiles(source.FullName, target.FullName);
                


            }
            catch (IOException ex)
            {
                Console.WriteLine("Error:" + ex.Message);
            }
        }


        public void CompareFiles(string SourceDirectory, string TargetDirectory)
        {
            DirectoryInfo source = new DirectoryInfo(SourceDirectory);
            DirectoryInfo target = new DirectoryInfo(TargetDirectory);
            State = false;

            foreach (FileInfo file in source.GetFiles())
            {
                Name = file.Name;
                string SourceFile = Path.Combine(source.FullName, file.Name);
                string TargetFile = Path.Combine(target.FullName, file.Name);


                if (System.IO.File.ReadAllBytes(TargetFile) == System.IO.File.ReadAllBytes(SourceFile))
                {
                    Console.WriteLine("The file " + Name + " already exists and is up to date");
                }
                else if (System.IO.File.ReadAllBytes(TargetFile) != System.IO.File.ReadAllBytes(SourceFile))
                {
                    var stopwatch = Stopwatch.StartNew();
                    Console.WriteLine("The file " + Name + " is updating");
                    System.IO.File.Delete(TargetFile);
                    System.IO.File.Copy(SourceFile, TargetFile, true);
                    stopwatch.Stop();
                }
                else
                {
                    CopyDirectory(SourceDirectory, TargetDirectory, Name);
                }
            }
        }
 
        public void CopyDirectory(string SourceDirectory, string TargetDirectory, string Name)
        {
            DirectoryInfo source = new DirectoryInfo(SourceDirectory);
            DirectoryInfo target = new DirectoryInfo(TargetDirectory);

            if (target.Exists == false)
            {
                Console.WriteLine("Folder Creation");
                target.Create();
            }
            Console.WriteLine("Initialisation");

            foreach (FileInfo file in source.GetFiles(Name))
            {
                var stopwatch = Stopwatch.StartNew();
                file.CopyTo(Path.Combine(target.FullName, file.Name));
                Name = file.Name;
                Date = DateTime.Now;
                stopwatch.Stop();
                Console.WriteLine("- " + Name + " : " + Date.ToString() + " - " + +stopwatch.Elapsed.Seconds + " seconds");
            }

        }
    }
}


