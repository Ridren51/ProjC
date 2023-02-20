using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Model.Backup
{
    public class TransferFile : ITransferFile
    {
        public string BackupName { get; set; }
        public string SourcePath { get; set; }
        public string TargetPath { get; set; }
        public long Size { get; set; }
        public double? TransferTime { get; set; }
        public TransferFile(string name, string sourcePath, string targetPath, long size, double? TransferTime)
        {
            BackupName = name;
            SourcePath = sourcePath;
            TargetPath = targetPath;
            Size = size;
            this.TransferTime = TransferTime;
        }

    }
}
