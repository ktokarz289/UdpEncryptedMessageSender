using System;
using System.Net;
using System.Net.Sockets;

namespace SimpleEncryptedChatServer
{
    class Program
    {
        static void Main(string[] args)
        {
			Console.WriteLine("Server Starting");
			while (true)
			{
				using (var client = new UdpClient(9000))
				{
					var ipEndPoint = new IPEndPoint(IPAddress.Any, 0);
					Byte[] receivedBytes = client.Receive(ref ipEndPoint);
					string message = System.Text.Encoding.ASCII.GetString(receivedBytes);
					Console.WriteLine($"Message received: {message}");
				}
			}
		}
    }
}
