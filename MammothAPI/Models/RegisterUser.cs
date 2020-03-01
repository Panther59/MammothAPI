// <copyright file="RegisterUser.cs" company="Ayvan">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>UTKARSHLAPTOP\Utkarsh</author>
// <date>2020-02-27</date>

namespace MammothAPI.Models
{
	/// <summary>
	/// Defines the <see cref="RegisterUser" />
	/// </summary>
	public class RegisterUser
	{
		/// <summary>
		/// Gets or sets the AuthCode
		/// </summary>
		public string AuthCode { get; set; }

		/// <summary>
		/// Gets or sets the Password
		/// </summary>
		public string Password { get; set; }

		/// <summary>
		/// Gets or sets the User
		/// </summary>
		public User User { get; set; }
	}
}
