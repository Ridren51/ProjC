using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySave_CLI.Model.Logs
{
    internal class JsonLog
    {
        public string Name { get; set; }
        public string SourceFilePath { get; set; }
        public string TargetFilePath { get; set; }
        public int? FileSize { get; set; }
        public double? FileTransferTime { get; set; }
        public string? State { get; set; }
        public int? TotalFilesToCopy { get; set; }
        public int? TotalFilesSize { get; set; }
        public int? NbFilesLeftToDo { get; set; }
        public int? Progression { get; set; }
        private string _dateFormat = "dd/MM/yyyy HH:mm:ss";
        public string Time { get; set; }

        public JsonLog(ITransferFile file, RealTimeLog log)
        {
            Name = file.Name;
            SourceFilePath = file.SourcePath;
            TargetFilePath = file.TargetPath;
            TotalFilesSize = log.TotalFilesSize;
            State = log.State;
            TotalFilesToCopy = log.TotalFiles;
            NbFilesLeftToDo = log.FilesLeft;
            Progression = 100 - (NbFilesLeftToDo - 1) * 100 / TotalFilesToCopy;
            Time = DateTime.Now.ToString(_dateFormat);
        }
        public JsonLog(ITransferFile file, DailyLog log)
        {
            Name = file.Name;
            SourceFilePath = file.SourcePath;
            TargetFilePath = file.TargetPath;
            FileSize = file.Size;
            FileTransferTime = file.TransferTime;
            Time = DateTime.Now.ToString(_dateFormat);
        }
    }

}
