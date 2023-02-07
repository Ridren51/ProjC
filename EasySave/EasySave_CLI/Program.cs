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
            init.doJob("D:\\BACK\\1", "D:\\BACK\\2");
        }
    }
}


