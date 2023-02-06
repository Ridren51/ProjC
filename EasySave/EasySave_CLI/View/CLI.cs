using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EasySave_CLI.View
{
    internal class CLI
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to EasySave CLI");
            Console.WriteLine("Enter 'help' to show every EasySave CLI commands");

            
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = cancellationTokenSource.Token;
            // Default value for the hour of the created log is set here
            Thread logThread = new Thread(() => CreateLog(cancellationToken, new TimeSpan(12, 00, 0)));
            logThread.Start();

            bool continueLoop = true;

            // Loop for the user's inputs
            while (continueLoop)
            {
                string? input = Console.ReadLine();
                input = (input == null) ? "" : input;
                if (input == "exit")
                {
                    continueLoop = false;

                    cancellationTokenSource.Cancel();
                }
                else if (input == "backup")
                {
                    Backup();
                }
                else if (input == "allbackups")
                {
                    RunAllBackups();
                }
                else if (input == "languages")
                {
                    ChangeLanguage();
                }
                else if (input == "logtime")
                {
                    ChangeLogTime(cancellationTokenSource, cancellationToken, logThread);
                }
                else if (input == "help")
                {
                    ShowCommands();
                }
                else
                {
                    Console.WriteLine("Invalid command");
                }
            }
            Console.WriteLine("Exiting EasySave CLI");
        }

        private static void ShowCommands()
        {
            string HelpString = "- 'backup' to backup a file\n- 'allbackups' to run every backups\n- 'language' to change the language of the application (available languages : FR, EN)\n- 'logtime' to change when the daily log gets created (12h00 by default)\n- 'exit' to exit the application";
            Console.WriteLine(HelpString);
        }

        private static void ChangeLanguage()
        {
            Console.WriteLine("Please enter the language you want to select : FR for french and EN for english");
            string? language = Console.ReadLine();
            while (language == null || (language.ToUpper() != "EN" && language.ToUpper() != "FR"))
            {
                Console.WriteLine("Please one of those two languages : FR for french and EN for english");
                language = Console.ReadLine();
            }

            //TODO : Code pour changer la langue ici

        }

        private static void RunAllBackups()
        {
            //TODO : Code pour lancer tous les backups ici
        }

        private static void ChangeLogTime(CancellationTokenSource cancellationTokenSource, CancellationToken cancellationToken, Thread logThread)
        {
            int logHour = -1;
            Console.WriteLine("Enter the hour at which the log should be created:");
            while (!(Int32.TryParse(Console.ReadLine(), out logHour)) || (logHour < 0 || logHour > 23))
            {
                Console.WriteLine("Please enter a number between 0 and 23");
            }

            int logMinute = -1;
            Console.WriteLine("Enter the minutes at which the log should be created:");
            while (!(Int32.TryParse(Console.ReadLine(), out logMinute)) || (logMinute < 0 || logMinute > 59))
            {
                Console.WriteLine("Please enter a number between 0 and 59");
            }

            cancellationTokenSource.Cancel();
            logThread.Join();
            cancellationTokenSource.Dispose();
            cancellationTokenSource = new CancellationTokenSource();
            cancellationToken = cancellationTokenSource.Token;
            logThread = new Thread(() => CreateLog(cancellationToken, new TimeSpan(logHour, logMinute, 0)));
            logThread.Start();
            Console.WriteLine("Time of creation of the log changed to " + logHour + "h" + ((logMinute >= 10) ? "" : "0") + logMinute + ".");
        }

        private static void Backup()
        {
            //TODO: Utiliser la classe Storage pour le path ?
            Console.WriteLine("Enter the file path to backup:");
            string? filePath = Console.ReadLine();
            filePath = (filePath == null) ? "" : filePath;

            Console.WriteLine("Backing up file: " + filePath);
            //TODO: Code pour faire le backup du fichier ici
        }

        private static void CreateLog(CancellationToken cancellationToken, TimeSpan logTime)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                DateTime now = DateTime.Now;
                DateTime nextLogTime = new DateTime(now.Year, now.Month, now.Day, logTime.Hours, logTime.Minutes, 0);

                if (nextLogTime <= now) nextLogTime = nextLogTime.AddDays(1);

                TimeSpan timeUntilNextLog = nextLogTime - now;

                int sleepTime = (int)timeUntilNextLog.TotalMilliseconds;

                if (sleepTime > 0)
                {
                    if (!cancellationToken.WaitHandle.WaitOne(sleepTime))
                    {
                        Console.WriteLine("Creating daily log");
                        //TODO: Code pour créer le log ici
                    }
                }
            }
        }
    }
    
}
