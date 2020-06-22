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
	using Microsoft.Extensions.Configuration;
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Data.SqlClient;
	using System.Linq;
	using System.Threading.Tasks;

	/// <summary>
	/// Defines the <see cref="ReportsService" />.
	/// </summary>
	public class ReportsService : IReportsService
	{
		/// <summary>
		/// Defines the configuration.
		/// </summary>
		private readonly IConfiguration configuration;

		/// <summary>
		/// Defines the mammothDBContext.
		/// </summary>
		private readonly MammothDBContext mammothDBContext;

		/// <summary>
		/// Defines the mappers.
		/// </summary>
		private readonly IMappers mappers;

		/// <summary>
		/// Initializes a new instance of the <see cref="ReportsService"/> class.
		/// </summary>
		/// <param name="configuration">The configuration<see cref="IConfiguration"/>.</param>
		/// <param name="mammothDBContext">The mammothDBContext<see cref="MammothDBContext"/>.</param>
		/// <param name="mappers">The mappers<see cref="IMappers"/>.</param>
		public ReportsService(
			IConfiguration configuration,
			MammothDBContext mammothDBContext,
			IMappers mappers)
		{
			this.configuration = configuration;
			this.mammothDBContext = mammothDBContext;
			this.mappers = mappers;
		}

		/// <summary>
		/// The DeleteOldDataAsync.
		/// </summary>
		/// <returns>The <see cref="Task"/>.</returns>
		public async Task DeleteOldDataAsync()
		{
			DateTime date = DateTime.Now.AddDays(-7);
			var productSales = await this.mammothDBContext.ProductSales
				.Where(x => x.BusinessDate < date)
				.ToListAsync();

			this.mammothDBContext.Database.SetCommandTimeout(300);
			this.mammothDBContext.ProductSales.RemoveRange(productSales);
			var storeSales = await this.mammothDBContext.Sales
				.Where(x => x.BusinessDate < date)
				.ToListAsync();

			this.mammothDBContext.Sales.RemoveRange(storeSales);
			await this.mammothDBContext.SaveChangesAsync();
		}

		/// <inheritdoc />
		public async Task<List<StoreSaleReport>> GetDataSubmitStatusAsync(DateTime businessDate)
		{
			var result = await this.GetSalesData(businessDate);

			return result.Select(x => new StoreSaleReport
			{
				Store = this.mappers.MapStore(x.Store),
				IsDataSubmitted = x.IsDataSumitted,
				//Sale = this.mappers.MapSale(x.StoreSale),
				//ProductsSale = x.ProductsSale.Select(y => this.mappers.MapSale(y)),
			}).ToList();
		}

		/// <summary>
		/// The GetTodaysSalesReportAsync.
		/// </summary>
		/// <param name="businessDate">The businessDate<see cref="DateTime"/>.</param>
		/// <returns>The <see cref="List{DataTable}"/>.</returns>
		public List<DataTable> GetTodaysSalesReportAsync(DateTime businessDate)
		{
			var param = new SqlParameter("@date", SqlDbType.Date);
			param.Value = businessDate.Date;
			var dataSet = this.ExecuteDataTableSqlDA(CommandType.StoredProcedure, "GetSalesReportData", new SqlParameter[1] { param });
			if (dataSet != null && dataSet.Tables != null)
			{
				List<DataTable> output = new List<DataTable>();
				foreach (DataTable table in dataSet.Tables)
				{
					output.Add(table);
				}

				return output;
			}

			return null;
		}

		/// <summary>
		/// The ExecuteDataTableSqlDA.
		/// </summary>
		/// <param name="cmdType">The cmdType<see cref="CommandType"/>.</param>
		/// <param name="cmdText">The cmdText<see cref="string"/>.</param>
		/// <param name="cmdParms">The cmdParms<see cref="SqlParameter[]"/>.</param>
		/// <returns>The <see cref="DataSet"/>.</returns>
		private DataSet ExecuteDataTableSqlDA(CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
		{
			using var sqlConnection = new SqlConnection(this.configuration.GetConnectionString("MammothDBConnectionString"));
			sqlConnection.Open();
			using var sqlCommand = new SqlCommand(cmdText, sqlConnection);
			sqlCommand.CommandType = cmdType;
			sqlCommand.Parameters.AddRange(cmdParms);
			var ds = new DataSet();
			using var da = new SqlDataAdapter(sqlCommand);
			da.Fill(ds);
			sqlConnection.Close();
			return ds;
		}

		/// <summary>
		/// The GetSalesData.
		/// </summary>
		/// <param name="businessDate">The businessDate<see cref="DateTime"/>.</param>
		/// <returns>The <see cref="Task{List{SalesDetails}}"/>.</returns>
		private async Task<List<SalesDetails>> GetSalesData(DateTime businessDate)
		{
			var stores = await this.mammothDBContext.Stores.Include(x => x.Login).Where(x => x.Login.IsActive).ToListAsync();
			var storeSales = await this.mammothDBContext.Sales.Where(x => x.BusinessDate == businessDate).ToListAsync();
			var productSales = await this.mammothDBContext.Sales.Where(x => x.BusinessDate == businessDate).GroupBy(x => x.StoreId).Select(x => new { StoreID = x.Key }).ToListAsync();

			var results = (from s in stores
						   join ss in storeSales on s.Id equals ss.StoreId into sss
						   from sj in sss.DefaultIfEmpty()
						   join ps in productSales on s.Id equals ps.StoreID into pss
						   from pj in pss.DefaultIfEmpty()
						   select new SalesDetails
						   {
							   Store = s,
							   IsDataSumitted = (sj != null && pj != null)
						   })
						   .OrderBy(x => x.Store.Code)
						   .ToList();

			//var result = await this.mammothDBContext.Stores
			//	.Include(x => x.Sales)
			//	.Include(x => x.ProductSales)
			//	.Select(x => new SalesDetails
			//	{
			//		Store = x,
			//		IsDataSumitted = x.Sales != null &&
			//		x.Sales.Any(x => x.BusinessDate == businessDate) &&
			//		x.ProductSales != null &&
			//		x.ProductSales.Any(x => x.BusinessDate == businessDate),
			//		StoreSale = x.Sales != null ? x.Sales.FirstOrDefault(x => x.BusinessDate == businessDate) : null,
			//		ProductsSale = x.ProductSales != null ? x.ProductSales.Where(x => x.BusinessDate == businessDate) : null,
			//	})
			//	.OrderBy(x => x.Store.Code)
			//	.ToListAsync();
			return results;
		}
	}
}
