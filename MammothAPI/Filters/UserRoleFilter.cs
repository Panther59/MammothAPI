using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MammothAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MammothAPI.Filters
{
	public class UserRoleFilter : IAuthorizationFilter
	{
		readonly string _claim;
		readonly TypeOfUser _value;

		public UserRoleFilter(string claim, TypeOfUser value)
		{
			_claim = claim;
			_value = value;
		}

		public void OnAuthorization(AuthorizationFilterContext context)
		{
			var inputValue = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == _claim);
			if (inputValue == null || !_value.HasFlag((TypeOfUser)Enum.Parse(typeof(TypeOfUser), inputValue.Value)))
			{
				context.Result = new ForbidResult();
			}
		}
	}
}
