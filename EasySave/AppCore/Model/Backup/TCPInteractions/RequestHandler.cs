using AppCore.Model.Backup;
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
        public void AddBackupJob(string name, string sourceDirectory, string targetDirectory, BackupEnum type)
        {
            BackupJobs.Add(new BackupJob(name, sourceDirectory, targetDirectory, type));
        }
        public void AddBackupJob(string name, string sourceDirectory, string targetDirectory, string type)
        {
            BackupEnum enumType;

            switch (type)
            {
                case "Full":
                    enumType = BackupEnum.Full;
                    break;
                case "Differential":
                    enumType = BackupEnum.Differential;
                    break;
                default:
                    enumType = BackupEnum.Full;
                    break;
            }
            AddBackupJob(name, sourceDirectory, targetDirectory, enumType);
        }
        public void RemoveBackupJob(int index)
        {
            try
            {
                BackupJobs.RemoveAt(index);
            }
            catch (ArgumentOutOfRangeException)
            {
                return;
            }
        }
        public BackupInfos GetBackupJob(int index)
        {
            return BackupJobs[index].GetBackupInfos();
        }
        public List<BackupInfos> GetAllBackups()
        {
            List<BackupInfos> BackupInfosList = new List<BackupInfos>();
            foreach (var job in BackupJobs)
            {
                BackupInfosList.Add(job.GetBackupInfos());
            }
            return BackupInfosList;
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
        }
        public async void RunAllBackups()
        {
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

        internal object GetBackupsNames()
        {
            List<string> names = new List<string>();
            foreach (var job in BackupJobs)
            {
                names.Add(job.ToString());
            }
            return names;
        }
    }
}
