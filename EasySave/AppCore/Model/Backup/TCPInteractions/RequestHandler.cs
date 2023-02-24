using AppCore.Model.Backup;
using AppCore.Model.Utilities;
using EasySave_CLI.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Model.TCPInteractions
{
    public class RequestHandler
    {
        private List<BackupJob> BackupJobs { get; set; }
        public RequestHandler()
        {
            BackupJobs = new List<BackupJob>();
        }
        public async void AddBackupJob(string name, string sourceDirectory, string targetDirectory, BackupEnum type)
        {
            await Task.Run(() =>
            {
                BackupJobs.Add(new BackupJob(name, sourceDirectory, targetDirectory, type, Config.CryptingExtension, Config.HeavyFileSize));
            });
        }
        public async void PauseBackup(int index)
        {
            await Task.Run(() =>
            {
                BackupJobs[index].PauseBackup();
            });
        }

        public async void ResumeBackup(int index)
        {
            await Task.Run(() =>
            {
                BackupJobs[index].ResumeBackup();
            });
        }
        public Boolean IsBackupQueueFull()
        {
            return BackupJobs.Count == 5;
        }

        public async void RemoveBackupJob(int index)
        {
            await Task.Run(() =>
            {
                try
                {
                    BackupJobs.RemoveAt(index);
                }
                catch (ArgumentOutOfRangeException)
                {
                    return;
                }
            });
        }
        public BackupInfos GetBackupJob(int index)
        {
            return BackupJobs[index].GetBackupInfos();
        }

        public List<BackupInfos> GetBackupJobs()
        {
            List<BackupInfos> Grid = new List<BackupInfos> { };
            for (int i = 0; i < BackupJobs.Count; i++)
            {
                Grid.Add(BackupJobs[i].GetBackupInfos());
            }
            return Grid;
        }


        public Boolean IsNameValid(string? name)
        {
            return !string.IsNullOrEmpty(name);
        }
        public Boolean IsDirectoryValid(string? directory)
        {
            return Directory.Exists(directory);
        }

        public async void RunSpecificBackup(int index)
        {
            await Task.Run(() =>
            {
                BackupJobs[index].StartBackup();
                History.WriteHistory(BackupJobs[index]);
            });
        }

        public async void RunAllBackups()
        {
            await Task.Run(() =>
            {
                List<BackupJob> backupJobsCopy = BackupJobs.ToList();
                foreach (BackupJob backup in backupJobsCopy)
                {
                    backup.StartBackup();
                    History.WriteHistory(backup);
                }
            });
        }

        public string GetEnumValues()
        {
            StringBuilder sb = new StringBuilder();

            foreach (int value in Enum.GetValues(typeof(BackupEnum)))
            {
                var name = Enum.GetName(typeof(BackupEnum), value);
                var descriptionAttribute = (System.ComponentModel.DescriptionAttribute)Attribute.GetCustomAttribute(typeof(BackupEnum).GetField(name), typeof(System.ComponentModel.DescriptionAttribute));
                sb.Append(value + ": " + name + Environment.NewLine);
            }

            return sb.ToString();
        }
        public BackupEnum GetBackupTypeByIndex(int index)
        {
            return (BackupEnum)Enum.ToObject(typeof(BackupEnum), index);
        }
    }
}
