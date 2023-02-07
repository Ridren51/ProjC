using EasySave_CLI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySave_CLI.ViewModel
{
    internal class Adapter
    {
        public List<BackupJob> BackupJobs { get; set; }
        public Adapter() {

            BackupJobs = new List<BackupJob>();
        }

    }
}
