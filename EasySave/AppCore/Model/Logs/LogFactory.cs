using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySave_CLI.Model.Logs
{
    internal class LogFactory
    {
        public static LogType getLogType(Log log)
        {
            switch(log.GetType()) {
                case Type t when t == typeof(RealTimeLog) : return LogType.RealTimeLog;
                case Type t when t == typeof(DailyLog): return LogType.DailyLog;
                default: throw new Exception();
            }
        }
    }
}
