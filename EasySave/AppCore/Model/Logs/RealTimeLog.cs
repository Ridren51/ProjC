using System.Text.Json;
using AppCore.Model.Backup;

namespace EasySave_CLI.Model.Logs
{
    public class RealTimeLog : Log
    {
        private string _state;
        private long _totalFilesSize;
        private int _totalFiles;
        private int _filesLeft;
        public string State { get { return _state; } set { _state = value; } }
        public int TotalFiles { get { return _totalFiles; } set { _totalFiles = value; } }
        public long TotalFilesSize { get { return _totalFilesSize; } set { _totalFilesSize = value; } }
        public int FilesLeft { get { return _filesLeft; } set { _filesLeft = value; } }
        public RealTimeLog(long totalFilesSize, int totalFiles)
        {
            _state = "INACTIVE";
            _totalFilesSize = totalFilesSize;
            _totalFiles = totalFiles;
            _filesLeft = totalFiles;
            _logPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
                "\\state";
        }
        public override void UpdateLog(ITransferFile file)
        {
            string pathToLog = _logPath + LogManager.logFormat.ToString().ToLower();
            if (!File.Exists(pathToLog))
                CreateLogFile();
            if (_filesLeft > 1)
                State = "ACTIVE";
            else
                State = "END";
            using (StreamWriter sw = new StreamWriter(pathToLog, true))
            {
                sw.WriteLine(getLog(file, LogManager.logFormat));
            }
            FilesLeft--;
        }

    }

}
