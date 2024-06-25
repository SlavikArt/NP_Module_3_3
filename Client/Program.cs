﻿using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                int port = 5555;
                TcpClient client = new TcpClient("127.0.0.1", port);
                Console.WriteLine("Connected to server!");
                NetworkStream stream = client.GetStream();

                string data = "Rock";
                byte[] sendData = Encoding.ASCII.GetBytes(data);
                await stream.WriteAsync(sendData, 0, sendData.Length);
                Console.WriteLine("Sent: " + data);

                byte[] responseData = new byte[256];
                int bytes = await stream.ReadAsync(responseData, 0, responseData.Length);
                string response = Encoding.ASCII.GetString(responseData, 0, bytes);
                Console.WriteLine("Received: " + response);

                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
    }
}