using EasySave_CLI.Model;
using EasySave_CLI.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void AddBackupJob(string name)
        {
            BackupJobs.Add(new BackupJob(name));
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

        private void GenerateEnglishLanguage()
        {
            var english = new Dictionary<string, string>
                {
                    {"Option", "Please choose an option:" },
                    {"HelpText", "1- Run backups\n2- Run specific backup\n3- Add backup job\n4- Remove backup job\n5- Show backup queue\n6- Restore backup\n7- Choose language\n8- exit\n" },
                    {"QueueFull", "Queue is full please delete a backup" },
                    {"EnterName", "Choose a name for the backup" },
                    {"InvalidName", "Please enter a name without special character or empty string" },
                    {"EnterIndex", "Enter the number of the backup to delete." }
                };
            ConsoleLanguage.AddLanguage("English", english);

        }
        private void GenerateFrenchLanguage()
        {
            var francais = new Dictionary<string, string>
                {
                    {"Option", "Choisissez une option:" },
                    {"HelpText", "1- Lancer les backups\n2- Lancer une backup specifique\n3- Ajouter une backup\n4- Supprimer une backup\n5- Afficher les backups\n6- Restaurer une backup\n7- Choisir une langue\n8- Quitter\n" },
                    {"QueueFull", "La file de backup est pleine, veuillez supprimer une backup svp" },
                    {"EnterName", "Choisissez un nom pour la backup" },
                    {"InvalidName", "Entrez un nom sans caractere special ou chaine de caractere vide svp" },
                    {"EnterIndex", "Entrez le numero de la backup a supprimer" }
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

    }
}
