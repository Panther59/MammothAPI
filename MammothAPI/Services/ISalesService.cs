// <copyright file="ISalesService.cs" company="Ayvan">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>UTKARSHLAPTOP\Utkarsh</author>
// <date>2020-03-02</date>

namespace MammothAPI.Services
{
	using MammothAPI.Models;
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	/// <summary>
	/// Defines the <see cref="ISalesService" />
	/// </summary>
	public interface ISalesService
	{
		/// <summary>
		/// The GetProductSalesAsync
		/// </summary>
		/// <param name="storeId">The storeId<see cref="int"/></param>
		/// <param name="date">The date<see cref="DateTime"/></param>
		/// <returns>The <see cref="Task{List{ProductSale}}"/></returns>
		Task<List<ProductSale>> GetProductSalesAsync(int storeId, DateTime date);

		/// <summary>
		/// The GetStoreSalesAsync
		/// </summary>
		/// <param name="storeId">The storeId<see cref="int"/></param>
		/// <param name="date">The date<see cref="DateTime"/></param>
		/// <returns>The <see cref="Task{StoreSale}"/></returns>
		Task<StoreSale> GetStoreSalesAsync(int storeId, DateTime date);

		/// <summary>
		/// The SaveProductSalesAsync
		/// </summary>
		/// <param name="storeId">The storeId<see cref="int"/></param>
		/// <param name="date">The date<see cref="DateTime"/></param>
		/// <param name="productSales">The productSales<see cref="List{ProductSale}"/></param>
		/// <returns>The <see cref="Task"/></returns>
		Task SaveProductSalesAsync(int storeId, DateTime date, List<ProductSale> productSales);

		/// <summary>
		/// The SaveStoreSalesAsync
		/// </summary>
		/// <param name="storeId">The storeId<see cref="int"/></param>
		/// <param name="date">The date<see cref="DateTime"/></param>
		/// <param name="sale">The sale<see cref="StoreSale"/></param>
		/// <returns>The <see cref="Task"/></returns>
		Task SaveStoreSalesAsync(int storeId, DateTime date, StoreSale sale);
	}
}
