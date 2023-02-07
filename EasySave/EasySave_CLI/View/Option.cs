using EasySave_CLI;
using EasySave_CLI.EasySave_CLI;
using System;
using System.Runtime.CompilerServices;

namespace EasySave_CLI
{
    public class App
    {
        public void Start()
        {

            RunMainMenu();
            Console.WriteLine("\r\n ___  __    ____   __ __   __   _   _  ___  \r\n| __|/  \\ /' _| `v' /' _/ /  \\ | \\ / || __| \r\n| _|| /\\ |`._`.`. .'`._`.| /\\ |`\\ V /'| _|  \r\n|___|_||_||___/ !_! |___/|_||_|  \\_/  |___| ");
            Console.WriteLine("\r\n\r\n Press any key to exit");
            Console.ReadKey(true);
        }

        public void RunMainMenu()
        {
            string prompt = "Welcome to EasySave";
            string[] options = { "Backups", "Settings" };
            Menu mainMenu = new Menu();
            mainMenu.Start(prompt, options);
            int selectedIndex = mainMenu.Run();

            switch (selectedIndex)
            {
                case 0:
                    BackupMode.RunBackupMode();
                    break;
                case 1:
                    LanguageOrLogs.RunLanguageOrLogs();
                    break;

            }


        }

    }
}
