using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }

}
