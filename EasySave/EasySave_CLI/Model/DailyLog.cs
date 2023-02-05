using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySave_CLI.Model
{
    internal class DailyLog : Log
    {
        public override Type LogType => typeof(DailyLog);
        public DailyLog()
        {
            _logPath = "D:\\daily.json";
        }

        public override void UpdateLog(ITransferFile file)
        {
            if (!File.Exists(_logPath))
                this.CreateLogFile();
            using (StreamWriter sw = new StreamWriter(_logPath, true))
            {
                sw.WriteLine(this.getLogJSON(file));
                sw.Close();
            }
        }

    }
}
