using EasySave_CLI.Model;
using System;
using System.IO;
using System.Threading;
using System.Xml.Schema;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

BackupJob init = new BackupJob();
init.doBackupJob();

namespace EasySave_CLI.Model
{
    class Program
    {
        static void Main(string[] args)
        {
            App myApp = new App();
            myApp.Start();
        }
    }
}


