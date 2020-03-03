// <copyright file="ProductSale.cs" company="Ayvan">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>UTKARSHLAPTOP\Utkarsh</author>
// <date>2020-03-01</date>

namespace MammothAPI.Models
{
	using System;

	/// <summary>
	/// Defines the <see cref="ProductSale" />
	/// </summary>
	public class ProductSale
	{
		/// <summary>
		/// Gets or sets the ProductID
		/// </summary>
		public Product Product { get; set; }

		/// <summary>
		/// Gets or sets the SaleCount
		/// </summary>
		public int? SaleCount { get; set; }
	}
}
