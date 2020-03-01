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
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
	using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
	using System.Threading.Tasks;

	/// <summary>
	/// Defines the <see cref="SalesController" />
	/// </summary>
	[ApiController]
	[Route("[controller]")]
	public class SalesController : ControllerBase
	{
		private readonly ISalesService salesService;
		private readonly ISession session;

		/// <summary>
		/// Defines the logger
		/// </summary>
		private readonly ILogger<SalesController> logger;

		/// <summary>
		/// Initializes a new instance of the <see cref="SalesController"/> class.
		/// </summary>
		/// <param name="productsService">The productsService<see cref="IProductsService"/></param>
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
		
		[HttpPost("products")]
		[StoreAuthorize]
		public async Task SaveProductSalesAsync(List<ProductSale> sales)
		{
			var businessDate = DateTime.Now;
			var storeId = this.session.StoreID.Value;
			await this.salesService.SaveProductSalesAsync(storeId, businessDate, sales);
		}

		[HttpGet("store")]
		[StoreAuthorize]
		public async Task<StoreSale> GetStoreSaleAsync()
		{
			var businessDate = DateTime.Now;
			var storeId = this.session.StoreID.Value;
			return await this.salesService.GetStoreSalesAsync(storeId, businessDate);
		}

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
