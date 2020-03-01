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
				.Include(x => x.Logins)
				.Where(x => x.Code.ToLower() == request.LoginName.ToLower() && x.Logins.Any(l => l.Password == encPassword))
				.FirstOrDefaultAsync();

			if (store != null)
			{
				return this.MapStore(store);
			}
			else
			{
				return null;
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
				.Include(x => x.LoginsUser)
				.Where(x => x.LoginName.ToLower() == request.LoginName.ToLower() && x.LoginsUser.Any(l => l.Password == encPassword))
				.FirstOrDefaultAsync();

			if (user != null)
			{
				return this.mappers.MapUser(user);
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// The MapStore
		/// </summary>
		/// <param name="store">The store<see cref="Stores"/></param>
		/// <returns>The <see cref="Store"/></returns>
		private Store MapStore(Stores store)
		{
			return new Store
			{
				ID = store.Id,
				Code = store.Code,
				Name = store.Name,
			};
		}
	}
}
