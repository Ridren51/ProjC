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
                "\\daily.json";
        }

        public override void UpdateLog(ITransferFile file)
        {
            if (!File.Exists(_logPath))
                CreateLogFile();
            using (StreamWriter sw = new StreamWriter(_logPath, true))
            {
                sw.WriteLine(getLog(file, LogManager.logFormat));
            }
        }

    }
}
