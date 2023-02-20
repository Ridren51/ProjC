using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore
{
    public class PathHandler
    {
        public static string BaseDirectory = Directory.GetCurrentDirectory();

        public static string getRelativePath(string path)
        {
            string baseDirectory = BaseDirectory.Substring(0, BaseDirectory.IndexOf("\\bin\\Debug\\net7.0"));
            string rootDirectory = Directory.GetParent(baseDirectory).FullName;

            string relativePath = Path.Combine(rootDirectory,"AppCore", path);
            return relativePath;
            
        }
    }
}
