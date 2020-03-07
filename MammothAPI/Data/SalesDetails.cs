// <copyright file="ReportsService.cs" company="Ayvan">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>UTKARSHLAPTOP\Utkarsh</author>
// <date>2020-03-02</date>

using System.Collections.Generic;

namespace MammothAPI.Data
{
	internal class SalesDetails
	{
		public Stores Store { get; set; }
		public bool IsDataSumitted { get; set; }
		public Sales StoreSale { get; set; }
		public IEnumerable<ProductSales> ProductsSale { get; set; }
	}
}