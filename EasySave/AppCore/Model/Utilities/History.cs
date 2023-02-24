using AppCore.Model.Backup;
using EasySave_CLI.Model.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Model.Utilities
{
    internal class History
    {
        private static string _historyPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\History.json";
        private static Semaphore _historyFile = new Semaphore(1, 1);

        public static void WriteHistory(BackupJob backup)
        {
            if (!File.Exists(_historyPath))
            {
                File.WriteAllText(_historyPath, "[\n]");
            }
            RemoveLastLine();
            string fileContent = File.ReadAllText(_historyPath);
            fileContent = fileContent.Replace(fileContent, fileContent + backup.ToJSON() + "," + "\n]");
                _historyFile.WaitOne();
        }
        public static void RemoveLastLine()
        {
            _historyFile.WaitOne();
            var lines = System.IO.File.ReadAllLines(_historyPath);
            System.IO.File.WriteAllLines(_historyPath, lines.Take(lines.Length - 1).ToArray());

        }

    }
}
