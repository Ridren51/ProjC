using EasySave_CLI.Model;
using EasySave_CLI.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EasySave_CLI.ViewModel
{
    internal class Adapter
    {
        public List<BackupJob> BackupJobs { get; set; }
        public ConsoleLanguage ConsoleLanguage { get; set; }
        public Adapter() {

            BackupJobs = new List<BackupJob>();
            BackupJobs.Capacity = 5;
            ConsoleLanguage = new ConsoleLanguage();
            GenerateEnglishLanguage();
            GenerateFrenchLanguage();
            ConsoleLanguage.SetLanguage("English");

        }

        public void AddBackupJob(string name, string sourceDirectory, string targetDirectory, BackupEnum type)
        {
            BackupJobs.Add(new BackupJob(name, sourceDirectory, targetDirectory, type));
        }

        public Boolean IsBackupQueueFull()
        {
            return BackupJobs.Count == 5;
        }

        public void RemoveBackupJob(int index)
        {
            try
            {
                BackupJobs.RemoveAt(index);
            } catch(ArgumentOutOfRangeException){
                return;
            }
        }
        public BackupJob GetBackupJob(int index) { 
            return BackupJobs[index];
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
            await BackupJobs[index].DoBackup();
            BackupJobs.RemoveAt(index);
        }
        public async void RunAllBackups()
        {
            List<BackupJob> backupJobsCopy = BackupJobs.ToList();
            foreach(BackupJob backup in backupJobsCopy)
            {
                await backup.DoBackup();
                BackupJobs.Remove(backup);
            }
        }

        public async void RunDifferencialBackup(int delay)
        {

        }

        private void GenerateEnglishLanguage()
        {
            var english = new Dictionary<string, string>
                {
                    {"Option", "Please choose an option:" },
                    {"HelpText", "1- Run backups\n2- Run specific backup\n3- Add backup job\n4- Remove backup job\n5- Show backup queue\n6- Restore backup\n7- Choose language\n8- exit\nUse help to show this message\n" },
                    {"QueueFull", "Queue is full please delete a backup" },
                    {"EnterName", "Choose a name for the backup" },
                    {"InvalidBackupArgument", "There was an error while creating the backup, check the name and path entered and please try again" },
                    {"EnterIndex", "Enter the number of the backup to delete" },
                    {"SourceDirectory", "Enter the source directory" },
                    {"TargetDirectory", "Enter the target directory" },
                    {"BackupType", "Select a backup type:\n 1- differential\n 2- standard" },
                    {"BackupDelay", "Enter differential backup delay" },
                    {"SpecificBackup", "Choose which backup to start" },
                };
            ConsoleLanguage.AddLanguage("English", english);

        }
        private void GenerateFrenchLanguage()
        {
            var francais = new Dictionary<string, string>
                {
                    {"Option", "Choisissez une option:" },
                    {"HelpText", "1- Lancer les backups\n2- Lancer une backup specifique\n3- Ajouter une backup\n4- Supprimer une backup\n5- Afficher les backups\n6- Restaurer une backup\n7- " +
                    "Choisir une langue\n8- Quitter\necrivez aide pour afficher ce message\n" },
                    {"QueueFull", "La file de backup est pleine, veuillez supprimer une backup svp" },
                    {"EnterName", "Choisissez un nom pour la backup" },
                    {"InvalidBackupArgument", "Une erreur est survenue lors de la creation de la backup, veuillez verifier les arguments et reessayer" },
                    {"EnterIndex", "Entrez le numero de la backup a supprimer" },
                    {"SourceDirectory", "Entrez le repertoire d entree" },
                    {"TargetDirectory", "Entrez le repertoire de sortie" },
                    {"BackupType", "Choisissez un type de backup:\n 1- differentielle\n 2- standard" },
                    {"BackupDelay", "Choisissez un delai pour la backup" },
                    {"SpecificBackup", "Choisissez quel backup lancer" },
                };
            ConsoleLanguage.AddLanguage("Francais", francais);

        }

        public void SetLanguage(string language)
        {
            ConsoleLanguage.SetLanguage(language);
        }
        
        public string GetFrenchLanguage()
        {
            return "Francais";
        }
        public string GetEnglishLanguage()
        {
            return "English";
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
