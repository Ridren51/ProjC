using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Model.Backup
{
    internal interface IBackupJob
    {
        public Task AsyncDoBackup();
        public void DoBackup();
    }
}
