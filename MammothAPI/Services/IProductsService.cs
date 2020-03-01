// <copyright file="IProductsService.cs" company="Ayvan">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>UTKARSHLAPTOP\Utkarsh</author>
// <date>2020-03-01</date>

namespace MammothAPI.Services
{
	using MammothAPI.Models;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	/// <summary>
	/// Defines the <see cref="IProductsService" />
	/// </summary>
	public interface IProductsService
	{
		/// <summary>
		/// The GetProductsAsync
		/// </summary>
		/// <returns>The <see cref="Task{List{Product}}"/></returns>
		Task<List<Product>> GetProductsAsync();
	}
}
