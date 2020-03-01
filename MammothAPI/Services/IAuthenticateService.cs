// <copyright file="AuthenticateService.cs" company="Ayvan">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>UTKARSHLAPTOP\Utkarsh</author>
// <date>2020-03-01</date>

using MammothAPI.Models;
using System.Threading.Tasks;

namespace MammothAPI.Services
{
	public interface IAuthenticateService
	{
		Task<Store> AuthenticateStore(AuthenticateRequest request);
		Task<User> AuthenticateUser(AuthenticateRequest request);
	}
}