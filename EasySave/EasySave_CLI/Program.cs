using System;
using System.IO;
using System.Threading;
using System.Xml.Schema;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConsoleBackupApp
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