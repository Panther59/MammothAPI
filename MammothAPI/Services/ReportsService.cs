// <copyright file="ReportsService.cs" company="Ayvan">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>UTKARSHLAPTOP\Utkarsh</author>
// <date>2020-03-02</date>

namespace MammothAPI.Services
{
	using MammothAPI.Data;
	using MammothAPI.Models;
	using Microsoft.EntityFrameworkCore;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	/// <summary>
	/// Defines the <see cref="ReportsService" />
	/// </summary>
	public class ReportsService : IReportsService
	{
		/// <summary>
		/// Defines the mammothDBContext
		/// </summary>
		private readonly MammothDBContext mammothDBContext;

		/// <summary>
		/// Defines the mappers
		/// </summary>
		private readonly IMappers mappers;

		/// <summary>
		/// Initializes a new instance of the <see cref="ReportsService"/> class.
		/// </summary>
		/// <param name="mammothDBContext">The mammothDBContext<see cref="MammothDBContext"/></param>
		/// <param name="mappers">The mappers<see cref="IMappers"/></param>
		public ReportsService(MammothDBContext mammothDBContext, IMappers mappers)
		{
			this.mammothDBContext = mammothDBContext;
			this.mappers = mappers;
		}

		/// <inheritdoc />
		public async Task<List<StoreSaleReport>> GetDataSubmitStatusAsync(DateTime businessDate)
		{
			var result = await this.mammothDBContext.Stores
				.Include(x => x.Sales)
				.Include(x => x.ProductSales)
				.Select(x => new
				{
					Store = x,
					IsDataSumitted = x.Sales != null &&
					x.Sales.Any(x => x.BusinessDate == businessDate) &&
					x.ProductSales != null &&
					x.ProductSales.Any(x => x.BusinessDate == businessDate),
					StoreSale = x.Sales != null ? x.Sales.FirstOrDefault(x => x.BusinessDate == businessDate) : null,
					ProductsSale = x.ProductSales != null ? x.ProductSales.Where(x => x.BusinessDate == businessDate) : null,
				}).ToListAsync();

			return result.Select(x => new StoreSaleReport
			{
				Store = this.mappers.MapStore(x.Store),
				IsDataSumitted = x.IsDataSumitted,
				Sale = this.mappers.MapSale(x.StoreSale),
				ProductsSale = x.ProductsSale.Select(y => this.mappers.MapSale(y)),
			}).ToList();
		}
	}
}
