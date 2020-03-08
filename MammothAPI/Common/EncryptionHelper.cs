// <copyright file="EncryptionHelper.cs" company="Ayvan">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>UTKARSHLAPTOP\Utkarsh</author>
// <date>2020-02-27</date>

namespace MammothAPI.Common
{
	using Microsoft.Extensions.Configuration;
	using System;
	using System.IO;
	using System.Security.Cryptography;
	using System.Text;

	/// <summary>
	/// Defines the <see cref="EncryptionHelper" />
	/// </summary>
	public class EncryptionHelper : IEncryptionHelper
	{
		/// <summary>
		/// The app settings
		/// </summary>
		private readonly IConfiguration configuration;

		/// <summary>
		/// Initializes a new instance of the <see cref="EncryptionHelper"/> class.
		/// </summary>
		/// <param name="configuration">The configuration<see cref="IConfiguration"/></param>
		public EncryptionHelper(IConfiguration configuration)
		{
			this.configuration = configuration;
		}

		/// <inheritdoc />
		public string Decrypt(string input)
		{
			string password = this.configuration.GetValue<string>("AppSettings:EncryptionPassword");
			return this.Decrypt(input, password);
		}

		/// <inheritdoc />
		public string Decrypt(string input, string password)
		{
			// Get the bytes of the string
			byte[] bytesToBeDecrypted = Convert.FromBase64String(input);
			byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
			passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

			byte[] bytesDecrypted = this.AES_Decrypt(bytesToBeDecrypted, passwordBytes);

			string result = Encoding.UTF8.GetString(bytesDecrypted);

			return result;
		}

		/// <inheritdoc />
		public string Encrypt(string input)
		{
			string password = this.configuration.GetValue<string>("AppSettings:EncryptionPassword");
			return this.Encrypt(input, password);
		}

		/// <inheritdoc />
		public string Encrypt(string input, string password)
		{
			// Get the bytes of the string
			byte[] bytesToBeEncrypted = Encoding.UTF8.GetBytes(input);
			byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

			// Hash the password with SHA256
			passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

			byte[] bytesEncrypted = this.AES_Encrypt(bytesToBeEncrypted, passwordBytes);

			string result = Convert.ToBase64String(bytesEncrypted);

			return result;
		}

		/// <summary>
		/// Decrypt the bytes using AES
		/// </summary>
		/// <param name="bytesToBeDecrypted">The bytes to be decrypted</param>
		/// <param name="passwordBytes">The password</param>
		/// <returns>The decrypted bytes</returns>
		private byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
		{
			byte[] decryptedBytes = null;

			// Set your salt here, change it to meet your flavor:
			// The salt bytes must be at least 8 bytes.
			byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

			using (MemoryStream ms = new MemoryStream())
			{
				using (RijndaelManaged aes = new RijndaelManaged())
				{
					aes.KeySize = 256;
					aes.BlockSize = 128;

					var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
					aes.Key = key.GetBytes(aes.KeySize / 8);
					aes.IV = key.GetBytes(aes.BlockSize / 8);

					aes.Mode = CipherMode.CBC;

					using (var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
					{
						cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
						cs.Close();
					}

					decryptedBytes = ms.ToArray();
				}
			}

			return decryptedBytes;
		}

		/// <summary>
		/// Execute AES encryption
		/// </summary>
		/// <param name="bytesToBeEncrypted">The bytes to be encrypted</param>
		/// <param name="passwordBytes">The password</param>
		/// <returns>The encrypted bytes</returns>
		private byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
		{
			byte[] encryptedBytes = null;

			// Set your salt here, change it to meet your flavor:
			// The salt bytes must be at least 8 bytes.
			byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

			using (MemoryStream ms = new MemoryStream())
			{
				using (RijndaelManaged aes = new RijndaelManaged())
				{
					aes.KeySize = 256;
					aes.BlockSize = 128;

					var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
					aes.Key = key.GetBytes(aes.KeySize / 8);
					aes.IV = key.GetBytes(aes.BlockSize / 8);

					aes.Mode = CipherMode.CBC;

					using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
					{
						cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
						cs.Close();
					}

					encryptedBytes = ms.ToArray();
				}
			}

			return encryptedBytes;
		}
	}
}
