// <copyright file="Product.cs" company="Ayvan">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>UTKARSHLAPTOP\Utkarsh</author>
// <date>2020-03-01</date>

namespace MammothAPI.Models
{
	/// <summary>
	/// Defines the <see cref="Product" />
	/// </summary>
	public class Product
	{
		/// <summary>
		/// Gets or sets the Group
		/// </summary>
		public string Group { get; set; }

		/// <summary>
		/// Gets or sets the ID
		/// </summary>
		public int ID { get; set; }

		/// <summary>
		/// Gets or sets the IsActive
		/// </summary>
		public bool? IsActive { get; set; }

		/// <summary>
		/// Gets or sets the Name
		/// </summary>
		public string Name { get; set; }
	}
}
