// <copyright file="CorsMiddleware.cs" company="Ayvan">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>UTKARSHLAPTOP\Utkarsh</author>
// <date>2020-03-02</date>

namespace MammothAPI.Common
{
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Cors.Infrastructure;
	using Microsoft.AspNetCore.Http;
	using System.Threading.Tasks;

	/// <summary>
	/// Defines the <see cref="CorsMiddleware" />
	/// </summary>
	public class CorsMiddleware
	{
		/// <summary>
		/// Defines the next
		/// </summary>
		private readonly RequestDelegate next;

		/// <summary>
		/// Initializes a new instance of the <see cref="CorsMiddleware"/> class.
		/// </summary>
		/// <param name="next">The next<see cref="RequestDelegate"/></param>
		public CorsMiddleware(RequestDelegate next)
		{
			this.next = next;
		}

		/// <summary>
		/// The Invoke
		/// </summary>
		/// <param name="httpContext">The httpContext<see cref="HttpContext"/></param>
		/// <returns>The <see cref="Task"/></returns>
		public Task Invoke(HttpContext httpContext)
		{
			if (!httpContext.Request.Headers.ContainsKey(CorsConstants.Origin))
			{
				return this.next(httpContext);
			}
			else
			{
				httpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
				httpContext.Response.Headers.Add("Access-Control-Allow-Credentials", "true");
				httpContext.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, X-CSRF-Token, X-Requested-With, Accept, Accept-Version, Content-Length, Content-MD5, Date, X-Api-Version, X-File-Name");
				httpContext.Response.Headers.Add("Access-Control-Allow-Methods", "POST,GET,PUT,PATCH,DELETE,OPTIONS");
				return this.next(httpContext);
			}
		}
	}

	/// <summary>
	/// Defines the <see cref="CorsMiddlewareExtensions" />
	/// </summary>
	public static class CorsMiddlewareExtensions
	{
		/// <summary>
		/// The UseCorsMiddleware
		/// </summary>
		/// <param name="builder">The builder<see cref="IApplicationBuilder"/></param>
		/// <returns>The <see cref="IApplicationBuilder"/></returns>
		public static IApplicationBuilder UseCorsMiddleware(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<CorsMiddleware>();
		}
	}
}
