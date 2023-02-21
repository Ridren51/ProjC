using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySave_CLI.Model.Storage
{
    internal class ExternalDisk : Storage
    {
        public ExternalDisk(string name, string path) : base(name, path)
        {
        }
    }
}
