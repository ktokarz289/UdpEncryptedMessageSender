using System;
using System.Net;
using System.Net.Sockets;
using UdpEncryptedMessageSenderLibrary;

namespace SimpleEncryptedChatClient
{
    class Program
    {
        static void Main(string[] args)
        {
			var message = String.Empty;
			Console.WriteLine("Enter a message to send");
			var encryption = new Encryption();
			while (message != "q")
			{
				message = Console.ReadLine();
				using (var client = new UdpClient())
				{
					client.Connect(IPAddress.Parse("127.0.0.1"), 9000);
					Byte[] data = encryption.Encrypt(message);
					client.Send(data, data.Length);
				}
			}
		}
    }
}
