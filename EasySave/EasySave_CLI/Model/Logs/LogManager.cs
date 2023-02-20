using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySave_CLI.Model.Logs
{
    public static class LogManager
    {
        public static LogFormat logFormat = LogFormat.JSON;
        private static DailyLog dailyLog = new DailyLog();

        public static RealTimeLog GetNewRealTimeLog(long totalFilesSize, int totalFiles)
        {
            return new RealTimeLog(totalFilesSize, totalFiles);
        }
        public static void UpdateDailyLog(ITransferFile file)
        {
            dailyLog.UpdateLog(file);
        }
    }
}
