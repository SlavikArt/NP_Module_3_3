using Server;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        TcpListener server = null;
        try
        {
            int port = 5555;
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");

            server = new TcpListener(localAddr, port);
            server.Start();

            while (true)
            {
                Console.WriteLine("Waiting for a connection... ");
                TcpClient client = await server.AcceptTcpClientAsync();
                Console.WriteLine("Connected!");

                ClientHandler handler = new ClientHandler(client);
                await handler.HandleClientAsync();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }
        finally
        {
            server.Stop();
        }
    }
}