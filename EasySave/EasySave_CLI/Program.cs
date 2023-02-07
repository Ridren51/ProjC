using EasySave_CLI.Model;
using System;



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


