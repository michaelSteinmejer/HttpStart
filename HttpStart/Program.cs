using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace HttpStart
{
    class Program
    {
        static void Main(string[] args)
        {
            // Dette er ip adressen
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            // Lytter på ip adressen og port nummer 7000
            TcpListener serverSocket = new TcpListener(ip, 7000);
            //starter server
            serverSocket.Start();
            Console.WriteLine("Server started!!");

            while (true)
            {
                // acceptere af der en ny forbindelse og sætter den til at være eb tcp client.
                TcpClient connectionSocket = serverSocket.AcceptTcpClient();
                Console.WriteLine("Server Activated!!");

                TCPService service = new TCPService(connectionSocket);

                Task.Factory.StartNew(service.doit);
            };
        }
    }
}
