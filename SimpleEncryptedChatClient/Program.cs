using System;
using System.Net;
using System.Net.Sockets;

namespace SimpleEncryptedChatClient
{
    class Program
    {
        static void Main(string[] args)
        {
			var message = String.Empty;
			Console.WriteLine("Enter a message to send");
			while (message != "q")
			{
				message = Console.ReadLine();
				using (var client = new UdpClient())
				{
					client.Connect(IPAddress.Parse("127.0.0.1"), 9000);
					Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
					client.Send(data, data.Length);
				}
			}
		}
    }
}
