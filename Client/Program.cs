using SimpleTCP;
namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SimpleTcpClient client = new SimpleTcpClient();
            Console.WriteLine("Siap-siap!");
            Console.ReadKey(true);
            try
            {
                client.Connect("localhost", 1000);
                client.Delimiter = 0x0A;
                client.DelimiterDataReceived += Client_DelimiterDataReceived;
                client.WriteAndGetReply("Hello Bro!\nYES\n");
                while (true)
                {
                    if (Console.KeyAvailable)
                    {
                        var c = Console.ReadKey(true);
                        if (c.Key == ConsoleKey.Escape) break;
                    }
                }
                client.Disconnect();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void Client_DelimiterDataReceived(object? sender, Message e)
        {
            Console.WriteLine("|"+e.MessageString+"|");
        }
    }
}