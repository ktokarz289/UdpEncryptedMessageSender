using System;
using System.Net;
using System.Net.Sockets;
using UdpEncryptedMessageSenderLibrary;

namespace SimpleEncryptedChatServer
{
    class Program
    {
        static void Main(string[] args)
        {
			Console.WriteLine("Server Starting");
			var encryption = new Encryption();
			while (true)
			{
				using (var client = new UdpClient(9000))
				{
					var ipEndPoint = new IPEndPoint(IPAddress.Any, 0);
					Byte[] receivedBytes = client.Receive(ref ipEndPoint);
					string message = encryption.Decrypt(receivedBytes);
					Console.WriteLine($"Message received: {message}");
				}
			}
		}
    }
}
