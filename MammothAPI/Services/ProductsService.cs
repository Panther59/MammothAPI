// <copyright file="ProductsService.cs" company="Ayvan">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>UTKARSHLAPTOP\Utkarsh</author>
// <date>2020-03-01</date>

namespace MammothAPI.Services
{
	using MammothAPI.Data;
	using MammothAPI.Models;
	using Microsoft.EntityFrameworkCore;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	/// <summary>
	/// Defines the <see cref="ProductsService" />
	/// </summary>
	public class ProductsService : IProductsService
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
		/// Initializes a new instance of the <see cref="ProductsService"/> class.
		/// </summary>
		/// <param name="mappers">The mappers<see cref="IMappers"/></param>
		/// <param name="mammothDBContext">The mammothDBContext<see cref="MammothDBContext"/></param>
		public ProductsService(
			IMappers mappers,
			MammothDBContext mammothDBContext)
		{
			this.mappers = mappers;
			this.mammothDBContext = mammothDBContext;
		}

		/// <inheritdoc />
		public async Task<List<Product>> GetProductsAsync()
		{
			var products = await this.mammothDBContext.Products
				.Include(x => x.Group)
				.ToListAsync();

			return products.Select(x => this.mappers.MapProduct(x)).ToList();

		}
	}
}
