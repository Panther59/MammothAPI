// <copyright file="StoreSaleReport.cs" company="Ayvan">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>UTKARSHLAPTOP\Utkarsh</author>
// <date>2020-03-02</date>

namespace MammothAPI.Models
{
	using System.Collections.Generic;

	/// <summary>
	/// Defines the <see cref="StoreSaleReport" />
	/// </summary>
	public class StoreSaleReport
	{
		/// <summary>
		/// Gets or sets a value indicating whether IsDataSubmitted
		/// </summary>
		public bool IsDataSubmitted { get; set; }

		/// <summary>
		/// Gets or sets the ProductsSale
		/// </summary>
		public IEnumerable<ProductSale> ProductsSale { get; set; }

		/// <summary>
		/// Gets or sets the Store
		/// </summary>
		public Store Store { get; set; }

		/// <summary>
		/// Gets or sets the Sale
		/// </summary>
		public StoreSale Sale { get; set; }
	}
}
