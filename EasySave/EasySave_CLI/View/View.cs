using EasySave_CLI.Model;
using EasySave_CLI.ViewModel;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySave_CLI.View
{
    public class View
    {
        private WPFAdapter _adapter;
        private ConsoleLanguage _consoleLanguage;
        private static Mutex mutex;
        private static View instance;
        private View()
        {
            _adapter = new WPFAdapter();
            _consoleLanguage = _adapter.ConsoleLanguage;
        }
        public static View Instance
        {
            get
            {
                    if (instance == null)
                    {
                        mutex = new Mutex(true, "CLIMutex", out bool createdNew);
                        if (!createdNew)
                            throw new ApplicationException("Another instance is already running.");
                        // Create the instance
                        instance = new View();
                    }
                    return instance;
            }
        }

        public void Start()
        {
            ShowHelp();
            while (true)
            {
                Console.WriteLine(_consoleLanguage.GetString("Option"));
                string? option = Console.ReadLine();
                if (int.TryParse(option, out int value))
                    ParseInput(value);
                else
                    ShowHelp();
            }

        }
        private void ParseInput(int option)
        {
            switch (option)
            {
                case 1:
                    RunBackups(); break;
                case 2:
                    RunSpecificBackup(); break;
                case 3:
                    AddBackupJob(); break;
                case 4:
                    DeleteBackupJob(); break;
                case 5:
                    this.ShowBackupQueue(); break;
                case 6:
                    Console.WriteLine("Not implemented yet"); break;
                case 7:
                    SetLanguage(); break;
                case 8:
                    System.Environment.Exit(0); break;
                case 9:
                    _adapter.PauseBackup(0);
                    break;
                case 10:
                    _adapter.ResumeBackup(0);
                    break;

                default:
                    Console.Clear(); ShowHelp(); break;
            }
        }
        private void ShowHelp()
        {
            Console.WriteLine("\r ___  __    ____   __ __   __   _   _  ___  \r\n| __|/  \\ /' _| `v' /' _/ /  \\ | \\ / || __| \r\n| _|| /\\ |`._`.`. .'`._`.| /\\ |`\\ V /'| _|  \r\n|___|_||_||___/ !_! |___/|_||_|  \\_/  |___| \n");
            Console.WriteLine(_consoleLanguage.GetString("HelpText"));

        }

        private void SetLanguage()
        {
            Console.WriteLine("1- English");
            Console.WriteLine("2- Francais");
            if (int.TryParse(Console.ReadLine(), out int language))
            {
                switch (language)
                {
                    case 1: _adapter.SetLanguage(_adapter.GetEnglishLanguage()); break;
                    case 2: _adapter.SetLanguage(_adapter.GetFrenchLanguage()); break;

                }
                Console.Clear();
                ShowHelp();
            }

        }

        private void AddBackupJob()
        {
            if (_adapter.IsBackupQueueFull())
            {
                Console.WriteLine(_consoleLanguage.GetString("QueueFull"));
                return;
            }
            Console.WriteLine(CLIAdapter.GetEnumValues());
            int type = int.Parse(Console.ReadLine());
            Console.WriteLine(_consoleLanguage.GetString("EnterName"));
            string name = Console.ReadLine();
            Console.WriteLine(_consoleLanguage.GetString("SourceDirectory"));
            string sourceDirectory = Console.ReadLine();
            Console.WriteLine(_consoleLanguage.GetString("TargetDirectory"));
            string targetDirectory = Console.ReadLine();


            if (_adapter.IsNameValid(name) && _adapter.IsDirectoryValid(sourceDirectory) && _adapter.IsDirectoryValid(targetDirectory))
                _adapter.AddBackupJob(name, sourceDirectory, targetDirectory,CLIAdapter.GetBackupTypeByIndex(type));
            else
                Console.WriteLine(_consoleLanguage.GetString("InvalidBackupArgument"));
        }

        private void DeleteBackupJob()
        {
            ShowBackupQueue();
            Console.WriteLine(_consoleLanguage.GetString("EnterIndex"));
            if (int.TryParse(Console.ReadLine(), out int index))
                _adapter.RemoveBackupJob(index - 1);
            else
                Console.WriteLine(_consoleLanguage.GetString("EnterIndex"));
        }

        private void ShowBackupQueue()
        {
            for (int i = 0; i < _adapter.BackupJobs.Count; i++)
            {
                Console.WriteLine(i + 1 + "- " + _adapter.GetBackupJob(i).ToString());
            }
        }

        private void RunBackups()
        {
            _adapter.RunAllBackups();
        }
        private void RunSpecificBackup()
        {
            ShowBackupQueue();
            Console.WriteLine(_consoleLanguage.GetString("SpecificBackup"));
            _adapter.RunSpecificBackup(int.Parse(Console.ReadLine()) - 1);
        }
    }
}
