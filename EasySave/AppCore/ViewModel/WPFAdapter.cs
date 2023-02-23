using AppCore.Model.Backup;
using AppCore.Model.Utilities;
using EasySave_CLI.View;
using System.Text;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using AppCore.Model;

namespace EasySave_CLI.ViewModel
{
    public class WPFAdapter
    {
        public List<BackupJob> BackupJobs { get; set; }
        public ConsoleLanguage ConsoleLanguage { get; set; }
        public WPFAdapter()
        {
            BackupJobs = new List<BackupJob>();
            BackupJobs.Capacity = 5;
            ConsoleLanguage = new ConsoleLanguage();
            ConsoleLanguage.SetLanguage("English");

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
        public BackupJob GetBackupJob(int index)
        {
            return BackupJobs[index];
        }

        public List<BackupJob> GetBackupJobs()
        {
            List<BackupJob> Grid = new List<BackupJob> { };
            for (int i = 0; i < BackupJobs.Count; i++)
            {
                Grid.Add(BackupJobs[i]);

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

        public void SetLanguage(string language)
        {
            ConsoleLanguage.SetLanguage(language);
        }

        public string GetFrenchLanguage()
        {
            return "Français";
        }
        public string GetEnglishLanguage()
        {
            return "English";
        }
        public string GetArabicLanguage()
        {
            return "Arabic";
        }
        public string GetItalianLanguage()
        {
            return "Italian";
        }
        static string GetEnumDescription(Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            var attributes = (System.ComponentModel.DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }

        public static string GetEnumValues()
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
        public static BackupEnum GetBackupTypeByIndex(int index)
        {
            return (BackupEnum)Enum.ToObject(typeof(BackupEnum), index);
        }


    }
}
