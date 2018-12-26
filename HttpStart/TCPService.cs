using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace HttpStart
{
    class TCPService
    {
        public TcpClient connectionSocket { get; set; }
        public string http { get; set; } = "HTTP/1.1 200 OK\r\n";
        public string Content { get; set; } = "Content-Type: text/html\r\n";
        public string Close { get; set; } = "Connection: close\r\n";
        public string RN { get; set; } = "\r\n";


        public TCPService(TcpClient connection)
        {
            this.connectionSocket = connection;

        }

        public void doit()
        {
            while (true)
            {
                Stream ns = connectionSocket.GetStream();
                StreamReader sr = new StreamReader(ns);
                StreamWriter sw = new StreamWriter(ns);

                sw.AutoFlush = true;
                

                while (true)
                {

                    string answer = "";
                    string message = sr.ReadLine();
                    sw.WriteLine(answer);

                    if (message.ToLower() == "stop")
                    {

                        answer = "connection closed";
                        sw.WriteLine(answer);
                        ns.Close();
                        sr.Close();
                        sw.Close();


                    }

                    Console.WriteLine(message);
                    
                    if (message.Contains("GET") && message.Contains("HTTP/1.1"))
                    {
                        var s = message.Split(' ', ' ')[0 + 1];
                       // answer = http + Content + Close + RN + "Hello Client" + RN;

                        var page = File.ReadAllText(Environment.CurrentDirectory + "/somefile.html");

                        answer = http + Content + Close + RN + page + RN;
                        sw.WriteLine(answer);
                    }
                }
            }
        }
    }
}
