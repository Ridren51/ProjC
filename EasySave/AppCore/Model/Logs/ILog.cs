using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Model.Backup;

namespace EasySave_CLI.Model.Logs
{
    internal interface ILog
    {
        public void UpdateLog(ITransferFile file);
    }
}
