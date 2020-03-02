// <copyright file="SalesService.cs" company="Ayvan">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>UTKARSHLAPTOP\Utkarsh</author>
// <date>2020-03-02</date>

namespace MammothAPI.Services
{
	using MammothAPI.Common;
	using MammothAPI.Data;
	using MammothAPI.Models;
	using Microsoft.EntityFrameworkCore;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	/// <summary>
	/// Defines the <see cref="SalesService" />
	/// </summary>
	public class SalesService : ISalesService
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
		/// Defines the session
		/// </summary>
		private readonly ISession session;

		/// <summary>
		/// Initializes a new instance of the <see cref="SalesService"/> class.
		/// </summary>
		/// <param name="mappers">The mappers<see cref="IMappers"/></param>
		/// <param name="mammothDBContext">The mammothDBContext<see cref="MammothDBContext"/></param>
		/// <param name="session">The session<see cref="ISession"/></param>
		public SalesService(
			IMappers mappers,
			MammothDBContext mammothDBContext,
			ISession session)
		{
			this.mappers = mappers;
			this.mammothDBContext = mammothDBContext;
			this.session = session;
		}

		/// <inheritdoc />
		public async Task<List<ProductSale>> GetProductSalesAsync(int storeId, DateTime date)
		{
			var sales = await this.mammothDBContext.ProductSales
				.Where(x => x.StoreId == storeId && x.BusinessDate == date)
				.ToListAsync();

			return sales.Select(x => this.mappers.MapSale(x)).ToList();
		}

		/// <inheritdoc />
		public async Task<StoreSale> GetStoreSalesAsync(int storeId, DateTime date)
		{
			var sale = await this.mammothDBContext.Sales
				.Where(x => x.BusinessDate == date && x.StoreId == storeId)
				.FirstOrDefaultAsync();

			return this.mappers.MapSale(sale);
		}

		/// <inheritdoc />
		public async Task SaveProductSalesAsync(int storeId, DateTime date, List<ProductSale> productSales)
		{
			var sales = productSales.Select(sale => new ProductSales
			{
				BusinessDate = date,
				ModifiedBy = this.session.LoginID.Value,
				ModifiedOn = DateTime.Now,
				ProductId = sale.ProductID.Value,
				SaleCount = sale.SaleCount.Value,
				StoreId = storeId,
			});

			var existingSales = await this.mammothDBContext.ProductSales
				.Where(x => x.StoreId == storeId && x.BusinessDate == date)
				.ToListAsync();

			this.mammothDBContext.RemoveRange(existingSales);
			this.mammothDBContext.AddRange(sales);
			await this.mammothDBContext.SaveChangesAsync();
		}

		/// <inheritdoc />
		public async Task SaveStoreSalesAsync(int storeId, DateTime date, StoreSale sale)
		{
			var existingSale = await this.mammothDBContext.Sales
				.Where(x => x.BusinessDate == date && x.StoreId == storeId)
				.FirstOrDefaultAsync();

			if (existingSale != null)
			{
				existingSale.Id = existingSale.Id;
				existingSale.CashDeposite = sale.CashDeposite;
				existingSale.DamageQuantity = sale.DamageQuantity;
				existingSale.MissingCrate = sale.MissingCrate;
				existingSale.MissingQuantity = sale.MissingQuantity;
				existingSale.OnlineOrder = sale.OnlineOrder;
				existingSale.OpenOrder = sale.OpenOrder;
				existingSale.PendingConsignment = sale.PendingConsignment;
				existingSale.PendingSapphireComplain = sale.PendingSapphireComplain;
				existingSale.PureMarquee = sale.PureMarquee;
				existingSale.TotalMarquee = sale.TotalMarquee;
				existingSale.TotalOnline = sale.TotalOnline;
				existingSale.LastModifiedBy = this.session.LoginID.Value;
				existingSale.LastModifiedOn = DateTime.Now;
				this.mammothDBContext.Entry(existingSale).State = EntityState.Modified;
			}
			else
			{
				var saleData = this.mappers.MapSale(sale);
				saleData.LastModifiedBy = this.session.LoginID.Value;
				saleData.LastModifiedOn = DateTime.Now;

				this.mammothDBContext.Sales.Add(saleData);
			}

			await this.mammothDBContext.SaveChangesAsync();
		}
	}
}
