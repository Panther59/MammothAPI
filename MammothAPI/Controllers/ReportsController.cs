// <copyright file="ReportsController.cs" company="Ayvan">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>UTKARSHLAPTOP\Utkarsh</author>
// <date>2020-03-04</date>

namespace MammothAPI.Controllers
{
	using MammothAPI.Filters;
	using MammothAPI.Models;
	using MammothAPI.Services;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.AspNetCore.Mvc;
    using OfficeOpenXml;
    using System;
	using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

	/// <summary>
	/// Defines the <see cref="ReportsController" />
	/// </summary>
	[ApiController]
	[Route("api/[controller]")]
	public class ReportsController : ControllerBase
	{
		private readonly IHostingEnvironment hostingEnvironment;
		/// <summary>
		/// Defines the reportsService
		/// </summary>
		private readonly IReportsService reportsService;

		/// <summary>
		/// Initializes a new instance of the <see cref="ReportsController"/> class.
		/// </summary>
		/// <param name="reportsService">The reportsService<see cref="IReportsService"/></param>
		public ReportsController(
				IHostingEnvironment hostingEnvironment,
		IReportsService reportsService)
		{
			this.hostingEnvironment = hostingEnvironment;
			this.reportsService = reportsService;
		}

		/// <summary>
		/// The GetStoreSaleAsync
		/// </summary>
		/// <returns>The <see cref="Task{List{StoreSaleReport}}"/></returns>
		[HttpGet("summary/{date}")]
		[UserAuthorize]
		public async Task<List<StoreSaleReport>> GetStoreSaleAsync(DateTime date)
		{
			return await this.reportsService.GetDataSubmitStatusAsync(date);
		}

		[HttpGet("report/{date}")]
		[HttpHead("report/{date}")]
		[UserAuthorize]
		public FileResult Export(DateTime date)
		{
			string folder = this.hostingEnvironment.ContentRootPath;
			string excelName = $"Report-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
			string downloadUrl = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, excelName);
			FileInfo file = new FileInfo(Path.Combine(folder, excelName));
			if (file.Exists)
			{
				file.Delete();
				file = new FileInfo(Path.Combine(folder, excelName));
			}

			var data = this.reportsService.GetTodaysSalesReportAsync(date);
			using (var package = new ExcelPackage(file))
			{
				var workSheet = package.Workbook.Worksheets.Add("Overall Sales");
				workSheet.Cells.LoadFromDataTable(data[0], true);

				var productsWorkSheet = package.Workbook.Worksheets.Add("Product Sales");
				productsWorkSheet.Cells.LoadFromDataTable(data[1], true);

				package.Save();
			}

			const string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
			HttpContext.Response.ContentType = contentType;
			HttpContext.Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");
			var bytes = System.IO.File.ReadAllBytes(file.FullName);
			var fileContentResult = new FileContentResult(bytes, contentType)
			{
				FileDownloadName = file.FullName
			};

			return fileContentResult;

		}
	}
}
