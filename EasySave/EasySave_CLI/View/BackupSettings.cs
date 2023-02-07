
using System;
using System.IO;

namespace EasySave_CLI
{
    public class BackupSettings
    {
        static public void Information()
        {
            Console.WriteLine("Enter the source file path:");
            string sourceFile = Console.ReadLine();
            Console.WriteLine("Enter the destination file path:");
            string destinationFile = Console.ReadLine();

            Console.WriteLine("\nSource file: " + sourceFile);
            Console.WriteLine("Destination file: " + destinationFile);
            Console.WriteLine("\nAre these inputs correct? (y/n)");
            string confirmation = Console.ReadLine();

            if (confirmation == "y")
            {
                try
                {
                    File.Copy(sourceFile, destinationFile, true);
                    Console.WriteLine("\nFile copied successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("\nAn error occurred while copying the file: " + ex.Message);
                }
            }
            else
            {
                Console.WriteLine("\nCopy operation cancelled by user.");
            }

            Console.WriteLine("\nPress any key to exit.");
            Console.ReadKey();
        }
    }
}

