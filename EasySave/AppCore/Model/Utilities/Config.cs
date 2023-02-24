using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AppCore.Model.Utilities
{
    internal class Config
    {
        private static List<string> _cryptingExtension = new();
        private static List<string> _priorityExtension = new();
        private static List<string> _softwareNames = new();
        private static double _heavyFileSize = 100000000;
        private static string _lang = "en";

        static Config()
        {
            LoadFromFile();
        }
        public static double HeavyFileSize
        {
            get { return _heavyFileSize; }
            set { _heavyFileSize = value; SaveToFile(); }
        }
        public static List<string> CryptingExtension
        {
            get { return _cryptingExtension; }
        }

        public static List<string> SoftwareNames
        {
            get { return _softwareNames; }
        }
        public static List<string> PriorityExtension
        {
            get { return _priorityExtension; }
        }
        public static string Lang
        {
            get { return _lang; }
            set { _lang = value; SaveToFile(); }
        }

        public static void RemoveCryptingExtension(string extension)
        {
            _cryptingExtension.Remove(extension);
            SaveToFile();
        }
        public static void RemovePriorityExtension(string extension)
        {
            _priorityExtension.Remove(extension);
            SaveToFile();
        }
        public static void RemoveSoftwareName(string extension)
        {
            _softwareNames.Remove(extension);
            SaveToFile();
        }
        public static void AddCryptingExtension(string extension)
        {
            _cryptingExtension.Add(extension);
            _cryptingExtension = _cryptingExtension.Distinct().ToList();
            SaveToFile();
        }
        public static void AddPriorityExtension(string extension)
        {
            _priorityExtension.Add(extension);
            _priorityExtension = _priorityExtension.Distinct().ToList();
            SaveToFile();
        }
        public static void AddSoftwareName(string name) 
        {
            _softwareNames.Add(name);
            _softwareNames = _softwareNames.Distinct().ToList();
            SaveToFile();
        }
        private static void SaveToFile()
        {
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\ConfigA.json";
            var config = new
            {
                _cryptingExtension = Config._cryptingExtension,
                _priorityExtension = Config._priorityExtension,
                _softwareNames = Config._softwareNames,
                _heavyFileSize = Config._heavyFileSize,
                Language = Config._lang,
            };
            var configJson = JsonSerializer.Serialize(config);

            File.WriteAllText(filePath, configJson);
        }
        public static void LoadFromFile()
        {
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\ConfigA.json";
            if (File.Exists(filePath))
            {
                var configJson = File.ReadAllText(filePath);
                //var config = JsonSerializer.Deserialize<dynamic>(configJson);
                using var document = JsonDocument.Parse(configJson);
                var root = document.RootElement;

                _cryptingExtension = root.GetProperty("_cryptingExtension").EnumerateArray().Select(x => x.GetString()).ToList()!;
                _priorityExtension = root.GetProperty("_priorityExtension").EnumerateArray().Select(x => x.GetString()).ToList()!;
                _softwareNames = root.GetProperty("_softwareNames").EnumerateArray().Select(x => x.GetString()).ToList()!;
                _heavyFileSize = root.GetProperty("_heavyFileSize").GetInt32();
                _lang = root.GetProperty("Language").GetString()!;
            }
        }

    }
}
