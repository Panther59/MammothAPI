// <copyright file="AuthenticateService.cs" company="Ayvan">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>UTKARSHLAPTOP\Utkarsh</author>
// <date>2020-03-01</date>

namespace MammothAPI.Services
{
	using MammothAPI.Common;
	using MammothAPI.Data;
	using MammothAPI.Models;
	using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
	using System.Threading.Tasks;

	/// <summary>
	/// Defines the <see cref="AuthenticateService" />
	/// </summary>
	public class AuthenticateService : IAuthenticateService
	{
		private readonly IMappers mappers;

		/// <summary>
		/// Defines the encryptionHelper
		/// </summary>
		private readonly IEncryptionHelper encryptionHelper;

		/// <summary>
		/// Defines the mammothDBContext
		/// </summary>
		private readonly MammothDBContext mammothDBContext;

		/// <summary>
		/// Initializes a new instance of the <see cref="AuthenticateService"/> class.
		/// </summary>
		/// <param name="encryptionHelper">The encryptionHelper<see cref="IEncryptionHelper"/></param>
		/// <param name="mammothDBContext">The mammothDBContext<see cref="MammothDBContext"/></param>
		public AuthenticateService(
			IMappers mappers,
			IEncryptionHelper encryptionHelper,
			MammothDBContext mammothDBContext)
		{
			this.mappers = mappers;
			this.encryptionHelper = encryptionHelper;
			this.mammothDBContext = mammothDBContext;
		}

		/// <summary>
		/// The AuthenticateStore
		/// </summary>
		/// <param name="request">The request<see cref="AuthenticateRequest"/></param>
		/// <returns>The <see cref="Task{Store}"/></returns>
		public async Task<Store> AuthenticateStore(AuthenticateRequest request)
		{
			var encPassword = this.encryptionHelper.Encrypt(request.Password);
			var store = await this.mammothDBContext.Stores
				.Include(x => x.Login)
				.Where(x => x.Code.ToLower() == request.LoginName.ToLower())
				.FirstOrDefaultAsync();

			if (store != null)
			{
				if (store.Login.Password == encPassword)
				{
					return this.mappers.MapStore(store);
				}
				else
				{
					throw new UnauthorizedAccessException("Invalid credential");
				}
			}
			else
			{
				throw new UnauthorizedAccessException("Login name doesn't exists");
			}
		}

		/// <summary>
		/// The AuthenticateUser
		/// </summary>
		/// <param name="request">The request<see cref="AuthenticateRequest"/></param>
		/// <returns>The <see cref="Task{User}"/></returns>
		public async Task<User> AuthenticateUser(AuthenticateRequest request)
		{
			var encPassword = this.encryptionHelper.Encrypt(request.Password);
			var user = await this.mammothDBContext.Users
				.Include(x => x.Login)
				.Where(x => x.LoginName.ToLower() == request.LoginName.ToLower())
				.FirstOrDefaultAsync();

			if (user != null)
			{
				if (user.Login.Password == encPassword)
				{
					return this.mappers.MapUser(user);
				}
				else
				{
					throw new UnauthorizedAccessException("Invalid credential");
				}
			}
			else
			{
				throw new UnauthorizedAccessException("Login name doesn't exists");
			}
		}
	}
}
