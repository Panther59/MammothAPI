// <copyright file="IReportsService.cs" company="Ayvan">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>UTKARSHLAPTOP\Utkarsh</author>
// <date>2020-03-07</date>

namespace MammothAPI.Services
{
	using MammothAPI.Models;
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Threading.Tasks;

	/// <summary>
	/// Defines the <see cref="IReportsService" />
	/// </summary>
	public interface IReportsService
	{
		/// <summary>
		/// The GetDataSubmitStatusAsync
		/// </summary>
		/// <param name="businessDate">The businessDate<see cref="DateTime"/></param>
		/// <returns>The <see cref="Task{List{StoreSaleReport}}"/></returns>
		Task<List<StoreSaleReport>> GetDataSubmitStatusAsync(DateTime businessDate);

		/// <summary>
		/// The GetTodaysSalesReportAsync
		/// </summary>
		/// <param name="businessDate">The businessDate<see cref="DateTime"/></param>
		/// <returns>The <see cref="List{DataTable}"/></returns>
		List<DataTable> GetTodaysSalesReportAsync(DateTime businessDate);
	}
}
