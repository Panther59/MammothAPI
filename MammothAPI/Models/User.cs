// <copyright file="User.cs" company="Ayvan">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>UTKARSHLAPTOP\Utkarsh</author>
// <date>2020-02-27</date>

namespace MammothAPI.Models
{
	/// <summary>
	/// Defines the <see cref="User" />
	/// </summary>
	public class User
	{
		/// <summary>
		/// Gets or sets the Email
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		/// Gets or sets the FirstName
		/// </summary>
		public string FirstName { get; set; }

		/// <summary>
		/// Gets or sets the ID
		/// </summary>
		public int? ID { get; set; }

		/// <summary>
		/// Gets or sets the IsActive
		/// </summary>
		public bool? IsActive { get; set; }

		/// <summary>
		/// Gets or sets the LastName
		/// </summary>
		public string LastName { get; set; }

		/// <summary>
		/// Gets or sets the LoginID
		/// </summary>
		public int? LoginID { get; internal set; }

		/// <summary>
		/// Gets or sets the LoginName
		/// </summary>
		public string LoginName { get; set; }

		/// <summary>
		/// Gets or sets the Type
		/// </summary>
		public TypeOfUser? Type { get; set; }
	}
}
