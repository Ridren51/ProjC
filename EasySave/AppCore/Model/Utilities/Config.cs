using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Model.Utilities
{
    internal class Config
    {
        public static List<string> CryptingExtension = new List<string>();
        public static List<string> PriorityExtension = new List<string>();
        public static List<string> SoftwareNames = new List<string>();
        public static double HeavyFileSize = 100000000;

        public static void AddCryptingExtension(string extension)
        {
            CryptingExtension.Add(extension);
            CryptingExtension = CryptingExtension.Distinct().ToList();
        }
        public static void AddPriorityExtension(string extension)
        {
            PriorityExtension.Add(extension);
            PriorityExtension = PriorityExtension.Distinct().ToList();
        }
        public static void AddSoftwareName(string name) 
        {
            SoftwareNames.Add(name);
            SoftwareNames.Distinct().ToList();
        }

    }
}
