using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EasySave_CLI.Model.TCPInteractions
{
    internal class MyTCPListener
    {
        static RequestHandler requestHandler;

        public static void StartListener()
        {
            TcpListener server = null;
            requestHandler = new RequestHandler();
            try
            {

                // Set the TcpListener on port 23805.
                Int32 port = 23805;
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");

                // TcpListener server = new TcpListener(port);
                server = new TcpListener(localAddr, port);

                // Start listening for client requests.
                server.Start();

                // Buffer for reading data
                Byte[] bytes = new Byte[256];
                string data = "";

                // Enter the listening loop.
                while (true)
                {
                    Console.Write("Waiting for a connection... ");

                    // Perform a blocking call to accept requests.
                    // You could also use server.AcceptSocket() here.
                    using TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");

                    // Get a stream object for reading and writing
                    NetworkStream stream = client.GetStream();


                    try
                    {
                        int i;

                        // Loop to receive all the data sent by the client.
                        while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                        {
                            // Translate data bytes to a ASCII string.
                            data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                            Console.WriteLine("Received: {0}", data);

                            // Process the data sent by the client.
                            data = data.ToUpper();

                            ProcessRequest(data.Split(':'), stream);
                        }
                    }
                    catch (System.IO.IOException e)
                    {
                        Console.WriteLine("System.IO.IOException: {0}", e);
                    }
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                server.Stop();
            }

            Console.WriteLine("\nHit enter to continue...");
            Console.Read();
        }

        private static void ProcessRequest(string[] request, NetworkStream stream)
        {
            switch (request[0])
            {
                case "RunBackups":
                {
                    break;
                }
                case "RunSpecificBackup":
                {
                    break;
                }
                case "AddBackupJob":
                {
                    break;
                }
                case "DeleteBackupJob":
                {
                    break;
                }
                case "SetLanguage":
                {
                    break;
                }
                case "GetLanguage":
                {
                    break;
                }
                case "GetAllBackups":
                {
                    break;
                }
                case "RestoreBackup":
                {
                    break;
                }
                case "GetBackupQueue":
                {
                    break;
                }
                case "SetLogTime":
                {
                    break;
                }
                case "GetLogTime":
                {
                    break;
                }
                default:
                {
                    break;
                }
            }
        }

        private void SendResponseLineByLine(string[] message, NetworkStream stream)
        {
            foreach (string line in message)
            {
                SendResponse(line, stream);
            }
        }
        private void SendResponse(string message, NetworkStream stream)
        {
            byte[] lineSent = System.Text.Encoding.ASCII.GetBytes(message);
            stream.Write(lineSent, 0, lineSent.Length);
        }
    }
}
