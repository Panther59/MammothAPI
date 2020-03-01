// <copyright file="ErrorHandlingMiddleware.cs" company="Ayvan">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>UTKARSHLAPTOP\Utkarsh</author>
// <date>2020-02-29</date>

namespace MammothAPI.Common
{
    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json;
    using System;
    using System.Net;
    using System.Threading.Tasks;

	/// <summary>
	/// Defines the <see cref="ErrorHandlingMiddleware" />
	/// </summary>
	public class ErrorHandlingMiddleware
	{
		/// <summary>
		/// Defines the next
		/// </summary>
		private readonly RequestDelegate next;

		/// <summary>
		/// Initializes a new instance of the <see cref="ErrorHandlingMiddleware"/> class.
		/// </summary>
		/// <param name="next">The next<see cref="RequestDelegate"/></param>
		public ErrorHandlingMiddleware(RequestDelegate next)
		{
			this.next = next;
		}

		/// <summary>
		/// The Invoke
		/// </summary>
		/// <param name="context">The context<see cref="HttpContext"/></param>
		/// <returns>The <see cref="Task"/></returns>
		public async Task Invoke(HttpContext context /* other dependencies */)
		{
			try
			{
				await next(context);
			}
			catch (Exception ex)
			{
				await HandleExceptionAsync(context, ex);
			}
		}

		/// <summary>
		/// The HandleExceptionAsync
		/// </summary>
		/// <param name="context">The context<see cref="HttpContext"/></param>
		/// <param name="ex">The ex<see cref="Exception"/></param>
		/// <returns>The <see cref="Task"/></returns>
		private static Task HandleExceptionAsync(HttpContext context, Exception ex)
		{
			var code = HttpStatusCode.InternalServerError; // 500 if unexpected

			if (ex is UnauthorizedAccessException) code = HttpStatusCode.Unauthorized;
			else if (ex is Exception) code = HttpStatusCode.BadRequest;

			var result = JsonConvert.SerializeObject(new { error = ex.Message });
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)code;
			return context.Response.WriteAsync(result);
		}
	}
}
