// <copyright file="AuthenticateService.cs" company="Ayvan">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>UTKARSHLAPTOP\Utkarsh</author>
// <date>2020-03-01</date>

namespace MammothAPI.Services
{
	using log4net.Core;
	using MammothAPI.Common;
	using MammothAPI.Data;
	using MammothAPI.Models;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Logging;
	using System;
	using System.Linq;
	using System.Threading.Tasks;

	/// <summary>
	/// Defines the <see cref="AuthenticateService" />.
	/// </summary>
	public class AuthenticateService : IAuthenticateService
	{
		/// <summary>
		/// Defines the encryptionHelper.
		/// </summary>
		private readonly IEncryptionHelper encryptionHelper;

		/// <summary>
		/// Defines the mammothDBContext.
		/// </summary>
		private readonly MammothDBContext mammothDBContext;

		/// <summary>
		/// Defines the mappers.
		/// </summary>
		private readonly IMappers mappers;
		private readonly ILogger<AuthenticateService> logger;

		/// <summary>
		/// Initializes a new instance of the <see cref="AuthenticateService"/> class.
		/// </summary>
		/// <param name="mappers">The mappers<see cref="IMappers"/>.</param>
		/// <param name="encryptionHelper">The encryptionHelper<see cref="IEncryptionHelper"/>.</param>
		/// <param name="mammothDBContext">The mammothDBContext<see cref="MammothDBContext"/>.</param>
		public AuthenticateService(
			IMappers mappers,
			ILogger<AuthenticateService> logger,
			IEncryptionHelper encryptionHelper,
			MammothDBContext mammothDBContext)
		{
			this.mappers = mappers;
			this.logger = logger;
			this.encryptionHelper = encryptionHelper;
			this.mammothDBContext = mammothDBContext;
		}

		/// <inheritdoc />
		public async Task<Store> AuthenticateStore(AuthenticateRequest request)
		{
			logger.LogInformation($"User is getting authenticate: {request.LoginName}");
			var encPassword = this.encryptionHelper.Encrypt(request.Password);
			var store = await this.mammothDBContext.Stores
				.Include(x => x.Login)
				.Where(x => x.Code.ToLower() == request.LoginName.ToLower())
				.FirstOrDefaultAsync();

			if (store != null)
			{
				if (store.Login.IsActive == false)
				{
					logger.LogError($"Store is no longer active: {request.LoginName}");
					throw new UnauthorizedAccessException("Store is no longer active");
				}
				else if (store.Login.Password == encPassword)
				{
					var login = store.Login;
					login.LastLogin = DateTime.Now;
					this.mammothDBContext.Entry(login).State = EntityState.Modified;
					await this.mammothDBContext.SaveChangesAsync();

					logger.LogInformation($"User is authorized: {request.LoginName}");
					return this.mappers.MapStore(store);
				}
				else
				{
					logger.LogError($"Invalid credential: {request.LoginName}");
					throw new UnauthorizedAccessException("Invalid credential");
				}
			}
			else
			{
				logger.LogError($"Login name doesn't exists: {request.LoginName}");
				throw new UnauthorizedAccessException("Login name doesn't exists");
			}
		}

		/// <inheritdoc />
		public async Task<User> AuthenticateUser(AuthenticateRequest request)
		{
			var encPassword = this.encryptionHelper.Encrypt(request.Password);
			var user = await this.mammothDBContext.Users
				.Include(x => x.Login)
				.Where(x => x.LoginName.ToLower() == request.LoginName.ToLower())
				.FirstOrDefaultAsync();

			if (user != null)
			{
				var p = this.encryptionHelper.Decrypt(user.Login.Password);
				if (user.Login.IsActive == false)
				{
					logger.LogError($"User is no longer active: {request.LoginName}");
					throw new UnauthorizedAccessException("User is no longer active");
				}
				else if (user.Login.Password == encPassword)
				{
					var login = user.Login;
					login.LastLogin = DateTime.Now;
					this.mammothDBContext.Entry(login).State = EntityState.Modified;
					await this.mammothDBContext.SaveChangesAsync();
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

		/// <inheritdoc />
		public async Task ChangePassword(ChangePassword input)
		{
			if (input.StoreID.HasValue)
			{
				var login = await this.mammothDBContext.Logins
					.Include(x => x.Stores)
					.Where(x => x.Stores.Id == input.StoreID.Value)
					.FirstOrDefaultAsync();
				if (login == null)
				{
					throw new Exception("Your can not find store login");
				}
				else
				{
					login.Password = this.encryptionHelper.Encrypt(input.NewPassword);
					this.mammothDBContext.Entry(login).State = EntityState.Modified;
					await this.mammothDBContext.SaveChangesAsync();
				}
			}
			else
			{
				var login = await this.mammothDBContext.Logins
					.Where(x => x.Id == input.LoginID.Value)
					.FirstOrDefaultAsync();
				if (login == null)
				{
					throw new UnauthorizedAccessException("Your login is incorrect");
				}
				else
				{
					if (login.Password == this.encryptionHelper.Encrypt(input.CurrentPassword))
					{
						login.Password = this.encryptionHelper.Encrypt(input.NewPassword);
						this.mammothDBContext.Entry(login).State = EntityState.Modified;
						await this.mammothDBContext.SaveChangesAsync();
					}
					else
					{
						throw new Exception("Your current password do not match");
					}
				}
			}
		}

		/// <inheritdoc />
		public async Task<AuthenticateResponse> GetCurrentLogin(int loginId)
		{
			var login = await this.mammothDBContext.Logins
				.Include(x => x.UsersLogin)
				.Include(x => x.Stores)
				.Where(x => x.Id == loginId)
				.FirstOrDefaultAsync();

			return new AuthenticateResponse
			{
				Store = this.mappers.MapStore(login.Stores),
				User = this.mappers.MapUser(login.UsersLogin),
			};
		}
	}
}
