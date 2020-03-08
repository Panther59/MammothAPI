// <copyright file="ChangePassword.cs" company="Ayvan">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>UTKARSHLAPTOP\Utkarsh</author>
// <date>2020-03-08</date>

namespace MammothAPI.Models
{
	/// <summary>
	/// Defines the <see cref="ChangePassword" />
	/// </summary>
	public class ChangePassword
	{
		/// <summary>
		/// Gets or sets the CurrentPassword
		/// </summary>
		public string CurrentPassword { get; set; }

		/// <summary>
		/// Gets or sets the LoginID
		/// </summary>
		public int? LoginID { get; set; }

		/// <summary>
		/// Gets or sets the NewPassword
		/// </summary>
		public string NewPassword { get; set; }

		/// <summary>
		/// Gets or sets the StoreID
		/// </summary>
		public int? StoreID { get; set; }
	}
}
