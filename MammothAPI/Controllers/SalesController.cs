// <copyright file="SalesController.cs" company="Ayvan">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>UTKARSHLAPTOP\Utkarsh</author>
// <date>2020-03-01</date>

namespace MammothAPI.Controllers
{
	using MammothAPI.Common;
	using MammothAPI.Filters;
	using MammothAPI.Models;
	using MammothAPI.Services;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Extensions.Logging;
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	/// <summary>
	/// Defines the <see cref="SalesController" />
	/// </summary>
	[ApiController]
	[Route("api/[controller]")]
	public class SalesController : ControllerBase
	{
		/// <summary>
		/// Defines the logger
		/// </summary>
		private readonly ILogger<SalesController> logger;

		/// <summary>
		/// Defines the salesService
		/// </summary>
		private readonly ISalesService salesService;

		/// <summary>
		/// Defines the session
		/// </summary>
		private readonly ISession session;

		/// <summary>
		/// Initializes a new instance of the <see cref="SalesController"/> class.
		/// </summary>
		/// <param name="salesService">The salesService<see cref="ISalesService"/></param>
		/// <param name="session">The session<see cref="ISession"/></param>
		/// <param name="logger">The logger<see cref="ILogger{SalesController}"/></param>
		public SalesController(
			ISalesService salesService,
			ISession session,
			ILogger<SalesController> logger)
		{
			this.salesService = salesService;
			this.session = session;
			this.logger = logger;
		}

		/// <summary>
		/// The GetAllProductsAsync
		/// </summary>
		/// <returns>The <see cref="Task{List{Product}}"/></returns>
		[HttpGet("products")]
		[StoreAuthorize]
		public async Task<List<ProductSale>> GetAllProductsAsync()
		{
			var businessDate = DateTime.Now;
			var storeId = this.session.StoreID.Value;
			return await this.salesService.GetProductSalesAsync(storeId, businessDate);
		}

		/// <summary>
		/// The GetStoreSaleAsync
		/// </summary>
		/// <returns>The <see cref="Task{StoreSale}"/></returns>
		[HttpGet("store")]
		[StoreAuthorize]
		public async Task<StoreSale> GetStoreSaleAsync()
		{
			var businessDate = DateTime.Now;
			var storeId = this.session.StoreID.Value;
			return await this.salesService.GetStoreSalesAsync(storeId, businessDate);
		}

		/// <summary>
		/// The SaveProductSalesAsync
		/// </summary>
		/// <param name="sales">The sales<see cref="List{ProductSale}"/></param>
		/// <returns>The <see cref="Task"/></returns>
		[HttpPost("products")]
		[StoreAuthorize]
		public async Task SaveProductSalesAsync(List<ProductSale> sales)
		{
			var businessDate = DateTime.Now;
			var storeId = this.session.StoreID.Value;
			await this.salesService.SaveProductSalesAsync(storeId, businessDate, sales);
		}

		/// <summary>
		/// The SaveStoreSaleAsync
		/// </summary>
		/// <param name="sale">The sale<see cref="StoreSale"/></param>
		/// <returns>The <see cref="Task"/></returns>
		[HttpPost("store")]
		[StoreAuthorize]
		public async Task SaveStoreSaleAsync(StoreSale sale)
		{
			var businessDate = DateTime.Now;
			var storeId = this.session.StoreID.Value;
			await this.salesService.SaveStoreSalesAsync(storeId, businessDate, sale);
		}
	}
}
