using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySave_CLI.Model
{
    internal interface IBackupJob
    {
      public Task DoBackup();
      public Task DoDifferiencialBackup();
    }
}
