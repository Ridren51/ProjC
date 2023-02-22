using AppCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EasySave_CLI.View
{
    public class ConsoleLanguage
    {
        private Dictionary<string, Dictionary<string, string>> _languages;
        private string _currentLanguage;

        public ConsoleLanguage()
        {
            _languages = new Dictionary<string, Dictionary<string, string>>();
            GenerateAllLanguages();
            _currentLanguage = "English";
        }

        public void AddLanguage(string languageName, Dictionary<string, string> language)
        {
            _languages[languageName] = language;
        }

        public void SetLanguage(string languageName)
        {
            if (_languages.ContainsKey(languageName))
            {
                _currentLanguage = languageName;
            }
        }

        public string GetString(string key)
        {
            if (_languages.ContainsKey(_currentLanguage) && _languages[_currentLanguage].ContainsKey(key))
            {
                return _languages[_currentLanguage][key];
            }

            return string.Empty;
        }

        private void GenerateLanguage(string path, string language)
        {
            var jsonString = File.ReadAllText(PathHandler.getRelativePath(path));
            var lang = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonString);
            this.AddLanguage(language, lang);
        }
        private void GenerateEnglishLanguage()
        {
            GenerateLanguage("AppCore\\Model\\Utilities\\Language\\english.json", "English");
        }

        private void GenerateFrenchLanguage()
        {
            GenerateLanguage("AppCore\\Model\\Utilities\\Language\\french.json", "Français");
        }
        private void GenerateItalianLanguage()
        {
            GenerateLanguage("AppCore\\Model\\Utilities\\Language\\italian.json", "Italian");
        }
        private void GenerateArabicLanguage()
        {
            GenerateLanguage("AppCore\\Model\\Utilities\\Language\\arabic.json", "Arabic");
        }
        private void GenerateAllLanguages()
        {
            GenerateEnglishLanguage();
            GenerateArabicLanguage();
            GenerateItalianLanguage();
            GenerateFrenchLanguage();
        }
    }

}
