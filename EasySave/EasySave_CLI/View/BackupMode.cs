using ConsoleBackupApp;
using ConsoleBackupApp.ConsoleBackupApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBackupApp
{
    public class BackupMode
    {
        static public void RunBackupMode()
        {
            string prompt = "Which mode would you like?";
            string[] options = { "Mode 1 : Full Backup", "Mode 2 : Differential Backup" };
            Menu BackupMode = new Menu();
            BackupMode.Start(prompt, options);
            int selectedIndex = BackupMode.Run();

            switch (selectedIndex)
            {
                case 0:
                    Console.WriteLine("You have selected Mode 1");
                    BackupSettings.Information();
                    break;
                case 1:
                    Console.WriteLine("You have selected Mode 2");
                    break;
            }
        }
    }
}