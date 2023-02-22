using EasySave_CLI.Model;
using EasySave_CLI.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySave_CLI.ViewModel
{
    internal interface IAdapter
    {
        public List<string> GetBackupsNames();

    }
}
