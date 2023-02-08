using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySave_CLI.Model
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
            this.BackupName=name;
            this.SourcePath=sourcePath;
            this.TargetPath=targetPath;
            this.Size=size;
            this.TransferTime = TransferTime;
        }

    }
}
