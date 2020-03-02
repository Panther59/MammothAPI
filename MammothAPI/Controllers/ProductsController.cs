// <copyright file="ProductsController.cs" company="Ayvan">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>UTKARSHLAPTOP\Utkarsh</author>
// <date>2020-03-01</date>

namespace MammothAPI.Controllers
{
	using MammothAPI.Models;
	using MammothAPI.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
	using Microsoft.Extensions.Logging;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	/// <summary>
	/// Defines the <see cref="ProductsController" />
	/// </summary>
	[ApiController]
	[Route("api/[controller]")]
	public class ProductsController : ControllerBase
	{
		/// <summary>
		/// Defines the logger
		/// </summary>
		private readonly ILogger<ProductsController> logger;

		/// <summary>
		/// Defines the productsService
		/// </summary>
		private readonly IProductsService productsService;

		/// <summary>
		/// Initializes a new instance of the <see cref="ProductsController"/> class.
		/// </summary>
		/// <param name="productsService">The productsService<see cref="IProductsService"/></param>
		/// <param name="logger">The logger<see cref="ILogger{ProductsController}"/></param>
		public ProductsController(
			IProductsService productsService,
			ILogger<ProductsController> logger)
		{
			this.productsService = productsService;
			this.logger = logger;
		}

		/// <summary>
		/// The GetAllProductsAsync
		/// </summary>
		/// <returns>The <see cref="Task{List{Product}}"/></returns>
		[HttpGet]
		[Authorize]
		public async Task<List<Product>> GetAllProductsAsync()
		{
			return await this.productsService.GetProductsAsync();
		}
	}
}
