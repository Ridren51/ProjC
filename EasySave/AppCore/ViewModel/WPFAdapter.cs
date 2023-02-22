using AppCore.Model.Backup;
using AppCore.Model.Utilities;
using EasySave_WPF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using AppCore.Model;


namespace EasySave_WPF.ViewModel
{
    public class WPFAdapter
    {
        public List<BackupJob> BackupJobs { get; set; }
        public WPFAdapter()
        {
        }
    }
}
