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
        private Adapter _adapter;
        private ConsoleLanguage _consoleLanguage;
        public View() {
            _adapter = new Adapter();
            _consoleLanguage = _adapter.ConsoleLanguage;
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
            }
                
        }
        private void ParseInput(int option)
        {
            switch (option)
            {
                case 1:
                    Console.WriteLine("1"); break;
                case 2:
                    Console.WriteLine("2"); break;
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
                switch(language)
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
            if (_adapter.IsBackupQueueFull()) {
                Console.WriteLine(_consoleLanguage.GetString("QueueFull"));
                return;
             }
            Console.WriteLine(_consoleLanguage.GetString("EnterName"));
            string name = Console.ReadLine();
            if (_adapter.IsNameValid(name))
                _adapter.AddBackupJob(name);
            else
                Console.WriteLine(_consoleLanguage.GetString("InvalidName"));
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
            for (int i = 0; i < _adapter.BackupJobs.Count; i++) {
                Console.WriteLine(i + 1 + "- " + _adapter.GetBackupJob(i).ToString());
             }
        }
    }
}
