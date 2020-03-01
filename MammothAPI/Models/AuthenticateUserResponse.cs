// <copyright file="AuthenticateUserResponse.cs" company="Ayvan">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>UTKARSHLAPTOP\Utkarsh</author>
// <date>2020-02-27</date>

namespace MammothAPI.Models
{
	/// <summary>
	/// Defines the <see cref="AuthenticateUserResponse" />
	/// </summary>
	public class AuthenticateUserResponse
	{
		/// <summary>
		/// Gets or sets the Token
		/// </summary>
		public string Token { get; set; }

		/// <summary>
		/// Gets or sets the User
		/// </summary>
		public User User { get; set; }
	}
}
