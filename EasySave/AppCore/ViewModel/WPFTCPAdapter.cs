using AppCore.Model;
using AppCore.Model.Backup;
using AppCore.Model.TCPInteractions;
using AppCore.Model.Utilities;
using EasySave_CLI.Model;
using EasySave_CLI.View;
using EasySave_CLI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AppCore.ViewModel
{
    public class WPFTCPAdapter
    {
        private MyTCPClient _tcPClient;
        public ConsoleLanguage ConsoleLanguage { get; set; }
        public List<BackupInfos> BackupInfos { get; set; }
        public WPFTCPAdapter()
        {
            _tcPClient = new MyTCPClient();
            ConsoleLanguage = new ConsoleLanguage();
            ConsoleLanguage.SetLanguage("English");
        }

        public void AddBackupJob(string name, string sourceDirectory, string targetDirectory, BackupEnum type)
        {
            _tcPClient.SendRequest("AddBackupJob;" + name + ";" + sourceDirectory + ";" + targetDirectory + ";" + Enum.GetName(type));
        }
        public void PauseBackup(int index)
        {
            _tcPClient.SendRequest("PauseBackup;" + index);
        }

        public void ResumeBackup(int index)
        {
            _tcPClient.SendRequest("ResumeBackup;" + index);
        }
        public Boolean IsBackupQueueFull()
        {
            return (Boolean) _tcPClient.SendRequest("IsBackupQueueFull");
        }

        public void RemoveBackupJob(int index)
        {
            _tcPClient.SendRequest("RemoveBackupJob;" + index);
        }
        public BackupInfos GetBackupJob(int index)
        {
            return (BackupInfos)_tcPClient.SendRequest("GetBackupJob;" + index);
        }

        private List<BackupInfos> GetBackupJobs()
        {
            return (List<BackupInfos>)_tcPClient.SendRequest("GetBackupJobs");
        }

        public List<BackupInfos> GetBackupInfos()
        {
            BackupInfos = GetBackupJobs();
            return new List<BackupInfos>(BackupInfos);
        }

        public Boolean IsNameValid(string? name)
        {
            return (Boolean)_tcPClient.SendRequest("IsNameValid;" + name);
        }
        public Boolean IsDirectoryValid(string? directory)
        {
            return (Boolean)_tcPClient.SendRequest("IsDirectoryValid;" + directory);
        }

        public void RunSpecificBackup(int index)
        {
            _tcPClient.SendRequest("RunSpecificBackup;" + index);
        }

        public void RunAllBackups()
        {
            _tcPClient.SendRequest("RunAllBackups");
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

        public string GetEnumValues()
        {
            return (string)_tcPClient.SendRequest("GetEnumValues");
        }
        public BackupEnum GetBackupTypeByIndex(int index)
        {
            return (BackupEnum)_tcPClient.SendRequest("GetBackupTypeByIndex;" + index);
        }


    }
}
