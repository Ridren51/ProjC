using EasySave_CLI.ViewModel;
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
        public View() {
            _adapter = new Adapter();
        }

        public void Start()
        {
            ShowHelp();
            while (true)
            {
                Console.WriteLine("Please choose an option: ");
                string option = Console.ReadLine();
                if (int.TryParse(option, out int value))
                    ParseInput(value);
            }
                
        }
        private void ParseInput(int option)
        {
            switch (option)
            {
                case 1:
                    Console.Clear();
                    ShowHelp();
                    break;
                case 2:
                    Console.WriteLine("2"); break;
                case 3:
                    Console.WriteLine("3"); break;
                case 4:
                    Console.WriteLine("4"); break;
                case 5:
                    Console.WriteLine("5"); break;
                case 6:
                    Console.WriteLine("6"); break;
                case 7:
                    break;
                case 8:
                    System.Environment.Exit(0); break;
                default:
                    ShowHelp(); break;
            }
        }
        private void ShowHelp()
        {
            Console.WriteLine("\r ___  __    ____   __ __   __   _   _  ___  \r\n| __|/  \\ /' _| `v' /' _/ /  \\ | \\ / || __| \r\n| _|| /\\ |`._`.`. .'`._`.| /\\ |`\\ V /'| _|  \r\n|___|_||_||___/ !_! |___/|_||_|  \\_/  |___| \n");
            Console.WriteLine("1- Run backups");
            Console.WriteLine("2- Run specific backup");
            Console.WriteLine("3- Add backup job");
            Console.WriteLine("4- Remove backup job");
            Console.WriteLine("5- Show backup queue");
            Console.WriteLine("6- Restore backup");
            Console.WriteLine("7- Choose language");
            Console.WriteLine("8- exit");

        }
    }
}
