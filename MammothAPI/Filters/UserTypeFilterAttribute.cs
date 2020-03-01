using Microsoft.AspNetCore.Mvc;
using MammothAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MammothAPI.Filters
{
	public class UserTypeFilterAttribute : TypeFilterAttribute
	{
		public UserTypeFilterAttribute(string claim, TypeOfUser value) : base(typeof(UserRoleFilter))
		{
			Arguments = new object[] { claim, value };
		}
	}
}
