using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Model.Backup;

namespace EasySave_CLI.Model.Logs
{
    public static class LogManager
    {
        public static LogFormat logFormat = LogFormat.JSON;
        private static DailyLog dailyLog = new DailyLog();
        private static Semaphore _writeDailyLog = new Semaphore(1, 1);

        public static RealTimeLog GetNewRealTimeLog(long totalFilesSize, int totalFiles)
        {
            return new RealTimeLog(totalFilesSize, totalFiles);
        }
        public static void UpdateDailyLog(ITransferFile file)
        {
            dailyLog.UpdateLog(file);
        }
        public static async Task UpdateRealTimeLog(RealTimeLog realtimelog, ITransferFile file)
        {
            await Task.Run(() =>
            {
                _writeDailyLog.WaitOne();
                try
                {
                    realtimelog.UpdateLog(file);
                }finally
                {
                    _writeDailyLog.Release();
                }
            });
        }
    }
}
