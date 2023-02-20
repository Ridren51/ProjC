using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySave_CLI.Model.Storage
{
    abstract class Storage
    {
        string Name { get; set; }
        string Path { get; set; }
        public Storage(String name, String path)
        {
            this.Name = name;
            this.Path = path;
        }
    }
}
