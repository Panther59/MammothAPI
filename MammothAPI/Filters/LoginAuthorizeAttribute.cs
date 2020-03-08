// <copyright file="LoginAuthorizeAttribute.cs" company="Ayvan">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>UTKARSHLAPTOP\Utkarsh</author>
// <date>2020-03-01</date>

namespace MammothAPI.Filters
{
	using MammothAPI.Common;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Mvc.Filters;

	/// <summary>
	/// Defines the <see cref="LoginAuthorizeAttribute" />
	/// </summary>
	public class LoginAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
	{
		/// <summary>
		/// The OnAuthorization
		/// </summary>
		/// <param name="context">The context<see cref="AuthorizationFilterContext"/></param>
		public void OnAuthorization(AuthorizationFilterContext context)
		{
			var session = context.HttpContext.RequestServices.GetService(typeof(ISession)) as ISession;

			if (session == null || session.LoginID.HasValue == false)
			{
				context.Result = new UnauthorizedResult();
			}
		}
	}
}
