// <copyright file="AuthenticateRequest.cs" company="Ayvan">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>UTKARSHLAPTOP\Utkarsh</author>
// <date>2020-02-27</date>

namespace MammothAPI.Models
{
	/// <summary>
	/// Defines the <see cref="AuthenticateRequest" />
	/// </summary>
	public class AuthenticateRequest
	{
		/// <summary>
		/// Gets or sets the LoginName
		/// </summary>
		public string LoginName { get; set; }

		/// <summary>
		/// Gets or sets the Password
		/// </summary>
		public string Password { get; set; }
	}
}
