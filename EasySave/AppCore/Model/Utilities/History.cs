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

        public static void WriteHistory(BackupJob backup)
        {
            using (StreamWriter sw = new StreamWriter(_historyPath, true))
            {
                sw.WriteLine(backup.ToJSON());
            }
        }

    }
}
