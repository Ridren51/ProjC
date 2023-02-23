using AppCore.Model.Backup;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySave_CLI.Model.Logs
{
    internal class DailyLog : Log
    {
        public DailyLog()
        {
            _logPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
                "\\daily.";
        }

        public override void UpdateLog(ITransferFile file)
        {
            string pathToLog = _logPath + LogManager.logFormat.ToString().ToLower();
            if (!File.Exists(_logPath))
                CreateLogFile();
            using (StreamWriter sw = new StreamWriter(pathToLog, true))
            {
                sw.WriteLine(getLog(file, LogManager.logFormat));
            }
        }

    }
}
