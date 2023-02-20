using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EasySave_CLI.Model.Logs
{
    public abstract class Log : ILog
    {
        protected string _logPath = "";
        protected delegate string GetLogDelegate(ITransferFile file);
        public abstract void UpdateLog(ITransferFile file);

        protected void CreateLogFile()
        {
            using (FileStream fs = File.Create(_logPath))
            {

            }
        }
        protected string getLogJSON(ITransferFile file)
        {
            LogType logType = LogFactory.getLogType(this);
            JsonLog jsonLog = logType switch
            {
                LogType.RealTimeLog => new JsonLog(file, (RealTimeLog)this),
                LogType.DailyLog => new JsonLog(file, (DailyLog)this),
                _ => throw new ArgumentException($"Invalid LogType: {logType}"),
            };
            JsonSerializerOptions options = new JsonSerializerOptions{
                WriteIndented = true,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            };
            string jsonString = JsonSerializer.Serialize(jsonLog, options);
            return jsonString;
        }
        protected string getLogXML(ITransferFile file)
        {

            var jsonDocument = JsonDocument.Parse(getLogJSON(file));
            var jsonRoot = jsonDocument.RootElement;

            XElement xml = new XElement("RealTimeLog");

            foreach (var property in jsonRoot.EnumerateObject())
            {
                xml.Add(new XElement(property.Name, property.Value.ToString()));
            }
            return xml.ToString();
        }

        protected string getLog(ITransferFile file, LogFormat logFormat)
        {
            GetLogDelegate getLogDelegate = null;
            switch (logFormat)
            {
                case LogFormat.JSON:
                    getLogDelegate = getLogJSON;
                    break;
                case LogFormat.XML:
                    getLogDelegate = getLogXML;
                    break;
                default:
                    throw new ArgumentException($"Invalid log format: {logFormat}");
            }
            return getLogDelegate(file);
        }
    }

}
