using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySave_CLI.Model
{
    internal class BacckupJob
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
                // Combiner les chemins d'accès pour obtenir le chemin complet du fichier source
                string SourceFile = Path.Combine(SourceDirectory, Name);

                // Combiner les chemins d'accès et ajouter la date et le type de sauvegarde pour obtenir le chemin complet du fichier cible
                string TargetFile = Path.Combine(TargetDirectory, Type + "_" + DateTime.Now.ToString("yyyyMMdd") + "_" + Name);

                // Copier le fichier de la source vers la cible
                File.Copy(SourceFile, TargetFile, true);

                Console.WriteLine("Le fichier a été sauvegardé avec succès.");
            }
            catch (IOException ex)
            {
                Console.WriteLine("Une erreur s'est produite lors de la sauvegarde du fichier : " + ex.Message);
            }
        }
        public void getBackupVersion()
        {

        }
    }
}


