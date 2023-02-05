﻿using System.Text.Json;

namespace EasySave_CLI.Model
{
    public class RealTimeLog : Log
    {
        public override Type LogType => typeof(RealTimeLog); 
        private string _state;
        private int _totalFilesSize;
        private int _totalFiles;
        private int _filesLeft;
        public string State { get { return _state; } set { _state = value; } }
        public int TotalFiles { get { return _totalFiles; } set { _totalFiles = value; } }
        public int TotalFilesSize { get { return _totalFilesSize; } set { _totalFilesSize = value; } }
        public int FilesLeft { get { return _filesLeft; } set { _filesLeft = value; } }

        public RealTimeLog(int totalFilesSize, int totalFiles)
        {
            _state = "INACTIVE";
            _totalFilesSize = totalFilesSize;
            _totalFiles = totalFiles;
            _filesLeft = totalFiles;
            _logPath = "D:\\log.json";
        }

        public override void UpdateLog(ITransferFile file)
        {
            if (!File.Exists(_logPath))
                this.CreateLogFile();
            if (_filesLeft > 1)
                State = "ACTIVE";
            else
                State = "END";
            using (StreamWriter sw = new StreamWriter(_logPath, true))
            {
                sw.WriteLine(this.getLogJSON(file));
            }
            FilesLeft--;
        }
       
    }
}
