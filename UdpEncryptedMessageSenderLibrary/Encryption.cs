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
				return new byte[20];
			}
		}

		private byte[] IV
		{
			get
			{
				return new byte[20];
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
								encryptedMessage = ms.ToArray();
							}
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
