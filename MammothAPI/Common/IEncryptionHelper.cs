// <copyright file="IEncryptionHelper.cs" company="Ayvan">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>UTKARSHLAPTOP\Utkarsh</author>
// <date>2020-02-27</date>

namespace MammothAPI.Common
{
	/// <summary>
	/// Defines the <see cref="IEncryptionHelper" />
	/// </summary>
	public interface IEncryptionHelper
	{
		/// <summary>
		/// Decrypts the encrypted string
		/// </summary>
		/// <param name="input">The encrypted string</param>
		/// <returns>The decrypted string</returns>
		string Decrypt(string input);

		/// <summary>
		/// Decrypts the encrypted string
		/// </summary>
		/// <param name="input">The encrypted string</param>
		/// <param name="password">The password</param>
		/// <returns>The decrypted string</returns>
		string Decrypt(string input, string password);

		/// <summary>
		/// Encrypt the string
		/// </summary>
		/// <param name="input">The input string</param>
		/// <returns>The encrypted string</returns>
		string Encrypt(string input);

		/// <summary>
		/// Encrypt the string
		/// </summary>
		/// <param name="input">The input string</param>
		/// <param name="password">The password</param>
		/// <returns>The encrypted string</returns>
		string Encrypt(string input, string password);
	}
}
