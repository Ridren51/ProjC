using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;

namespace AppCore.Model.TCPInteractions
{
    public class MyTCPListener
    {
        private static RequestHandler requestHandler;

        public static void StartListener()
        {
            TcpListener? server = null;
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
                //string data = "";

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
                            // Translate data bytes to a Unicode string.
                            var message = Encoding.Unicode.GetString(bytes, 0, i);
                            try
                            {
                                var data = Newtonsoft.Json.JsonConvert.DeserializeObject<object>(message);

                                if (data is string)
                                {
                                    Console.WriteLine("Received on server: {0}", (string)data);
                                    ProcessRequest(((string)data).Split(':'), stream);
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Exception deserializing message on server : {0}", e);
                            }
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
                if(server != null)
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
                    requestHandler.RunAllBackups();
                    SendResponse("All backups are running", stream);
                    break;
                }
                case "RunSpecificBackup":
                {
                    requestHandler.RunSpecificBackup(Int32.Parse(request[1]));
                    break;
                }
                case "AddBackupJob":
                {
                    requestHandler.AddBackupJob(request[1], request[2], request[3], request[4]);
                    break;
                }
                case "DeleteBackupJob":
                {
                    requestHandler.RemoveBackupJob(Int32.Parse(request[1]));
                    break;
                }
                case "GetAllBackups":
                {
                    SendResponse(requestHandler.GetAllBackups(), stream);
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

        private static void SendResponse(object response, NetworkStream stream)
        {
            // Send the response back to the client.
            var message = Newtonsoft.Json.JsonConvert.SerializeObject(response);
            var buffer = Encoding.Unicode.GetBytes(message);
            stream.Write(buffer, 0, buffer.Length);
        }
    }
}
