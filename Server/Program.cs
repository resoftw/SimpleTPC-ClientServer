using SimpleTCP;
using System.Net.Sockets;

namespace Server
{
    internal class Program
    {
        static List<TcpClient> tcpClients = new List<TcpClient>();
        static void Main(string[] args)
        {
            SimpleTcpServer server = new SimpleTcpServer().Start(1000);
            server.ClientConnected += Server_ClientConnected;
            server.Delimiter = 0x0A;
            server.DelimiterDataReceived += Server_DelimiterDataReceived;
            server.ClientDisconnected += Server_ClientDisconnected;
            Console.WriteLine("Listening...!");
            while (true)
            {
                Console.Write(".");
                foreach(var c in tcpClients)
                {
                    if (c.Connected)
                    {
                        c.Client.Send(new byte[] { (byte)'.', (byte)'\n' });
                    }
                }
                Thread.Sleep(1000);
            }
        }

        private static void Server_ClientDisconnected(object? sender, TcpClient e)
        {
            tcpClients.Remove(e);
            Console.WriteLine("Client disconnected!");
        }

        private static void Server_DelimiterDataReceived(object? sender, Message e)
        {
            Console.WriteLine("\n|"+e.MessageString+"|");
            e.ReplyLine("OK");
        }

        private static void Server_ClientConnected(object? sender, TcpClient e)
        {
            tcpClients.Add(e);
            Console.WriteLine("Client connected {0}", e.Client.RemoteEndPoint);
        }
    }
}