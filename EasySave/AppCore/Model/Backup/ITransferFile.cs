using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Model.Backup
{
    public interface ITransferFile
    {
        public string BackupName { get; set; }
        public string SourcePath { get; set; }
        public string TargetPath { get; set; }
        public long Size { get; set; }
        public double? TransferTime { get; set; }
        public int? CryptingTime { get; set; }

    }
}
