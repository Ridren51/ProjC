using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySave_CLI.Model
{
    internal enum BackupEnum
    {
        [System.ComponentModel.Description("diff")]
        Differential = 1,
        [System.ComponentModel.Description("full")]
        Full = 2,
    }
}
