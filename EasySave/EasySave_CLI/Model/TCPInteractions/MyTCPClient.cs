using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

class MyTCPClient
{
    private TcpClient? Client;

    public MyTCPClient()
    {
        try
        {
            Client = new TcpClient();
            // Connecter le client au serveur TcpListener
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
            if(Client != null && Client.Connected)
            {
                if (request == null) { request = string.Empty; }

                // Si l'utilisateur entre "quit", quitter la boucle
                if (request == "quit")
                {
                    Client.Close();
                    Console.WriteLine("Exited TCPClient");
                    return;
                }

                // Obtenir un flux de données pour envoyer des données au serveur
                NetworkStream stream = Client.GetStream();

                // Convertir le message en un tableau d'octets
                byte[] data = System.Text.Encoding.ASCII.GetBytes(request);

                // Envoyer les données au serveur
                stream.Write(data, 0, data.Length);

                Console.WriteLine("Sent: {0}", request);

                // recevoir une reponse
                //request =...
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
                default:
                {
                    break;
                }
            }
    }
}