using System;
using System.IO;
using System.Security.Cryptography;

namespace UdpEncryptedMessageSenderLibrary
{
    public class Encryption
    {
		private byte[] Key
		{
			get
			{
				return new byte[] { 0xbc, 0x97, 0x2d, 0x1c, 0x0e, 0x15, 0x01, 0x2d, 0x8a, 0xe1, 0x2f, 0x60, 0x06, 0x08, 0x68, 0x51, 0x0a, 0x18, 0x58, 0xe8, 0x17, 0xab, 0x19, 0x30, 0x2a, 0xe9, 0x89, 0xcb, 0x16, 0xa5, 0xab, 0x99};
			}
		}

		private byte[] IV
		{
			get
			{
				return new byte[] { 0x12, 0x91, 0x39, 0xd2, 0xde, 0x41, 0x7a, 0xbc, 0x2b, 0xe7, 0x89, 0x68, 0x2d, 0xc4, 0xcd, 0xf9 };
			}
		}

		public byte[] Encrypt(string message)
		{
			byte[] encryptedMessage;
			using (var aes = Aes.Create())
			{
				aes.Key = Key;
				aes.IV = IV;

				using (var encryptor = aes.CreateEncryptor())
				{
					using (var ms = new MemoryStream())
					{
						using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
						{
							using (var sw = new StreamWriter(cs))
							{
								sw.Write(message);
							}

							encryptedMessage = ms.ToArray();
						}
					}
				}
			}

			return encryptedMessage;
		}

		public string Decrypt(byte[] encryptedMessage)
		{
			string message;

			using (var aes = Aes.Create())
			{
				aes.Key = Key;
				aes.IV = IV;

				using (var decryptor = aes.CreateDecryptor())
				{
					using (var ms = new MemoryStream(encryptedMessage))
					{
						using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
						{
							using (var sr = new StreamReader(cs))
							{
								message = sr.ReadToEnd();
							}
						}
					}
				}
			}

			return message;
		}
	}
}
