using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySave_CLI.Model
{
    internal class File : IFile
    {
        public string Name { get; set; }
        public string SourcePath { get; set; }
        public string TargetPath { get; set; }
        public int Size { get; set; }
        public File(string name, string sourcePath, string targetPath, int size)
        {
            this.Name=name;
            this.SourcePath=sourcePath;
            this.TargetPath=targetPath;
            this.Size=size;
        }

    }
}
