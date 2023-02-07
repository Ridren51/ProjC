using EasySave_CLI.Model;
using System;
using System.IO;
using System.Threading;
using System.Xml.Schema;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;



namespace EasySave_CLI.Model
{
    class Program
    {
        static void Main(string[] args)
        {
            BackupJob init = new BackupJob("coucou","CC");
            //init.doBackup("D:\\BACK\\1", "D:\\BACK\\2");
            init.SetTime(1);
            init.doDifferiencialBackup("D:\\BACK2\\1", "D:\\BACK2\\2");
        }
    }
}


