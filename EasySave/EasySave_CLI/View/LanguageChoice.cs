using ConsoleBackupApp;
using ConsoleBackupApp.ConsoleBackupApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBackupApp
{
    public class LanguageOrLogs
    {
        static public void RunLanguageOrLogs()
        {
            string prompt = "What language would you like?";
            string[] options = { "French", "English" };
            Menu languageMenu = new Menu();
            languageMenu.Start(prompt, options);
            int selectedIndex = languageMenu.Run();

            switch (selectedIndex)
            {
                case 0:
                    Console.WriteLine("You have selected French");
                    break;
                case 1:
                    Console.WriteLine("You have selected English");
                    break;
            }
        }
    }
}
