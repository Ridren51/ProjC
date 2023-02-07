using System;

namespace EasySave_CLI
{
    public class BackupOptions
    {
        public void RunBackupOptions()
        {
            Console.WriteLine("Enter the date for backup (yyyy-MM-dd):");
            string backupDate = Console.ReadLine();

            Console.WriteLine("Enter the source file path:");
            string sourceFile = Console.ReadLine();

            Console.WriteLine("Enter the destination file path:");
            string destinationFile = Console.ReadLine();

            Console.WriteLine("\nBackup Summary:\n");
            Console.WriteLine("Date: " + backupDate);
            Console.WriteLine("Source: " + sourceFile);
            Console.WriteLine("Destination: " + destinationFile);

            Console.WriteLine("\nDo you want to proceed with the backup? (y/n)");
            string confirmation = Console.ReadLine();

            if (confirmation.ToLower() == "y")
            {
                Console.WriteLine("\nStarting backup...");
                // Add code to perform backup here
            }
            else
            {
                Console.WriteLine("\nBackup cancelled.");
            }
        }
    }
}
