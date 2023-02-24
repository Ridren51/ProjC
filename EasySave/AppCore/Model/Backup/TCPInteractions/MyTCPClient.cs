using AppCore.Model;
using AppCore.Model.Backup;
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
                System.Diagnostics.Debug.WriteLine("Connected to server!");
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
        }

        public object? SendRequest(string? request)
        {
            object? response = null;
            try
            {
                if (Client != null && Client.Connected)
                {
                    if (request == null) { request = string.Empty; }

                    // If the user inputs "quit", quit the application
                    if (request == "quit")
                    {
                        Client.Close();
                        return response;
                    }

                    // Get the stream to send data to the server
                    NetworkStream stream = Client.GetStream();

                    //  Send the request to the connected TcpServer.
                    var message = Newtonsoft.Json.JsonConvert.SerializeObject(request);
                    var buffer = Encoding.Unicode.GetBytes(message);
                    stream.Write(buffer, 0, buffer.Length);

                    //Console.WriteLine("Sent: {0}", request);

                    // Get a response from the server
                    response = GetResponse(stream, request.Split(';'));
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            return response;
        }

        private object? GetResponse(NetworkStream stream, string[] request)
        {
            object? responseFromServer = null;
            switch (request[0])
            {
                case "IsBackupQueueFull":
                {
                    object isFull = WaitForResponse(stream, typeof(Boolean));
                    if (isFull is Boolean)
                    {
                        responseFromServer = isFull;
                    }
                    break;
                }
                case "GetBackupJob":
                {
                    object backup = WaitForResponse(stream, typeof(List<BackupInfos>));
                    if (backup is BackupInfos)
                    {
                        responseFromServer = backup;
                    }
                    break;
                }
                case "GetBackupJobs":
                {
                    object listOfBackups = WaitForResponse(stream, typeof(List<BackupInfos>));
                    if (listOfBackups is List<BackupInfos>)
                    {
                        responseFromServer = listOfBackups;
                    }
                    break;
                }
                case "IsNameValid":
                {
                    object isValid = WaitForResponse(stream, typeof(Boolean));
                    if (isValid is Boolean)
                    {
                        responseFromServer = isValid;
                    }
                    break;
                }
                case "IsDirectoryValid":
                {
                    object isValid = WaitForResponse(stream, typeof(Boolean));
                    if (isValid is Boolean)
                    {
                        responseFromServer = isValid;
                    }
                    break;
                }
                case "GetEnumValues":
                {
                    object enumValues = WaitForResponse(stream, typeof(string));
                    if (enumValues is string)
                    {
                        responseFromServer = enumValues;
                    }
                    break;
                }
                case "GetBackupTypeByIndex":
                {
                    object backupType = WaitForResponse(stream, typeof(BackupEnum));
                    if (backupType is BackupEnum)
                    {
                        responseFromServer = backupType;
                    }
                    break;
                }
                default:
                {
                    break;
                }
            }

            return responseFromServer;
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