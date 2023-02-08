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
        public override Type LogType => typeof(DailyLog);
        public DailyLog()
        {
            _logPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
                "\\daily--" + DateTime.Now.ToString("dd-MM-yyyy--HH-mm-ss") + ".json";
        }

        public override void UpdateLog(ITransferFile file)
        {
            if (!File.Exists(_logPath))
                CreateLogFile();
            using (StreamWriter sw = new StreamWriter(_logPath, true))
            {
                sw.WriteLine(getLogJSON(file));
                sw.Close();
            }
        }

    }
}
