using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Model.Backup
{
    public enum BackupEnum
    {
        [System.ComponentModel.Description("diff")]
        Differential = 1,
        [System.ComponentModel.Description("full")]
        Full = 2,
    }
}
