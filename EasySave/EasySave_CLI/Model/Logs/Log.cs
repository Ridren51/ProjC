using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EasySave_CLI.Model.Logs
{
    public abstract class Log : ILog
    {
        protected string _logPath = "";
        public abstract Type LogType { get; }

        public abstract void UpdateLog(ITransferFile file);

        protected void CreateLogFile()
        {
            using (FileStream fs = File.Create(_logPath))
            {

            }
        }
        protected string getLogJSON(ITransferFile file)
        {
            JsonLog jsonLog;
            jsonLog = LogType == typeof(RealTimeLog) ? jsonLog = new JsonLog(file, (RealTimeLog)this) : jsonLog = new JsonLog(file, (DailyLog)this);
            JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true, DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull };
            string jsonString = JsonSerializer.Serialize(jsonLog, options);
            return jsonString;
        }
    }

}
