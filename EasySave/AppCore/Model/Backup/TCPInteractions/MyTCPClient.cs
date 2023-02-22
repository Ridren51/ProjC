using AppCore.Model;
using System;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;

namespace AppCore.Model.TCPInteractions
{
    public class MyTCPClient
    {
        private TcpClient? Client;

        public MyTCPClient()
        {
            try
            {
                Client = new TcpClient();
                // Connect the client to the TCP Listener
                Client.Connect("127.0.0.1", 23805);

                Console.WriteLine("Connected to server!");
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
        }

        public void SendRequest(string? request)
        {
            try
            {
                if (Client != null && Client.Connected)
                {
                    if (request == null) { request = string.Empty; }

                    // If the user inputs "quit", quit the application
                    if (request == "quit")
                    {
                        Client.Close();
                        return;
                    }

                    // Get the stream to send data to the server
                    NetworkStream stream = Client.GetStream();

                    //  Send the request to the connected TcpServer.
                    var message = Newtonsoft.Json.JsonConvert.SerializeObject(request);
                    var buffer = Encoding.Unicode.GetBytes(message);
                    stream.Write(buffer, 0, buffer.Length);

                    Console.WriteLine("Sent: {0}", request);

                    // Get a response from the server
                    GetResponse(stream, request.Split(':'));
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
        }

        private void GetResponse(NetworkStream stream, string[] request)
        {
            switch (request[0])
            {
                case "RunBackups":
                    {
                        object responseFromServer = WaitForResponse(stream, typeof(string));
                        if (responseFromServer is string)
                        {
                            Console.WriteLine("Received on client : " + responseFromServer);
                        }
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
                case "GetAllBackups":
                    {
                        object listOfBackups = WaitForResponse(stream, typeof(List<BackupInfos>));
                        if (listOfBackups is List<BackupInfos>)
                        {
                            foreach (object backupInfo in (List<BackupInfos>)listOfBackups)
                            {
                                Console.WriteLine("backupname : " + ((BackupInfos)backupInfo).BackupName +
                                    ", pathSource = " + ((BackupInfos)backupInfo).SourceDir +
                                    ", pathTarget = " + ((BackupInfos)backupInfo).TargetDir +
                                    ", type = " + ((BackupInfos)backupInfo).BackupType +
                                    ", index = " + ((BackupInfos)backupInfo).Index);
                            }
                        }
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
                default:
                    {
                        break;
                    }
            }
        }

        private object WaitForResponse(NetworkStream stream, Type typeOfAnswerExpected)
        {
            object? response = new object();

            Byte[] bytes = new Byte[8096];

            // Translate data bytes to a Unicode string.
            var message = Encoding.Unicode.GetString(bytes, 0, stream.Read(bytes, 0, bytes.Length));
            try
            {
                response = Newtonsoft.Json.JsonConvert.DeserializeObject(message, typeOfAnswerExpected);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception deserializing message on server : {0}", e);
            }
            if (response == null) response = new object();
            return response;
        }
    }
}