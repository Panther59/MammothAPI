// <copyright file="AuthenticateController.cs" company="Ayvan">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>UTKARSHLAPTOP\Utkarsh</author>
// <date>2020-03-01</date>

namespace MammothAPI.Controllers
{
	using MammothAPI.Common;
	using MammothAPI.Filters;
	using MammothAPI.Models;
	using MammothAPI.Services;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.Logging;
	using Microsoft.IdentityModel.Tokens;
	using System;
	using System.IdentityModel.Tokens.Jwt;
	using System.Security.Claims;
	using System.Text;
	using System.Threading.Tasks;

	/// <summary>
	/// Defines the <see cref="AuthenticateController" />.
	/// </summary>
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class AuthenticateController : ControllerBase
	{
		/// <summary>
		/// Defines the authenticateService.
		/// </summary>
		private readonly IAuthenticateService authenticateService;

		/// <summary>
		/// Defines the configuration.
		/// </summary>
		private readonly IConfiguration configuration;

		/// <summary>
		/// Defines the logger.
		/// </summary>
		private readonly ILogger<AuthenticateController> logger;

		/// <summary>
		/// Defines the session.
		/// </summary>
		private readonly ISession session;

		/// <summary>
		/// Initializes a new instance of the <see cref="AuthenticateController"/> class.
		/// </summary>
		/// <param name="session">The session<see cref="ISession"/>.</param>
		/// <param name="configuration">The configuration<see cref="IConfiguration"/>.</param>
		/// <param name="logger">The logger<see cref="ILogger{AuthenticateController}"/>.</param>
		/// <param name="authenticateService">The authenticateService<see cref="IAuthenticateService"/>.</param>
		public AuthenticateController(
			ISession session,
			IConfiguration configuration,
			ILogger<AuthenticateController> logger,
			IAuthenticateService authenticateService)
		{
			this.session = session;
			this.configuration = configuration;
			this.logger = logger;
			this.authenticateService = authenticateService;
		}

		/// <summary>
		/// The AuthenticateStoreAsync.
		/// </summary>
		/// <param name="request">The request<see cref="AuthenticateRequest"/>.</param>
		/// <returns>The <see cref="Task{AuthenticateResponse}"/>.</returns>
		[HttpPost]
		[ActionName("store")]
		public async Task<AuthenticateResponse> AuthenticateStoreAsync(AuthenticateRequest request)
		{
			var store = await this.authenticateService.AuthenticateStore(request);

			if (store == null)
			{
				throw new UnauthorizedAccessException("Login credentials are invalid");
			}

			return new AuthenticateResponse
			{
				Token = this.GenerateJSONWebToken(store),
				Store = store,
				Type = "Store"
			};
		}

		/// <summary>
		/// The AuthenticateUserAsync.
		/// </summary>
		/// <param name="request">The request<see cref="AuthenticateRequest"/>.</param>
		/// <returns>The <see cref="Task{AuthenticateUserResponse}"/>.</returns>
		[HttpPost]
		[ActionName("user")]
		public async Task<AuthenticateResponse> AuthenticateUserAsync(AuthenticateRequest request)
		{
			var user = await this.authenticateService.AuthenticateUser(request);

			if (user == null)
			{
				throw new UnauthorizedAccessException("Login credentials are invalid");
			}

			return new AuthenticateResponse
			{
				Token = this.GenerateJSONWebToken(user),
				User = user,
				Type = "User"
			};
		}

		/// <summary>
		/// The ChangePassword.
		/// </summary>
		/// <param name="input">The input<see cref="ChangePassword"/>.</param>
		/// <returns>The <see cref="Task"/>.</returns>
		[HttpPost]
		[ActionName("changePassword")]
		[LoginAuthorize]
		public async Task ChangePassword(ChangePassword input)
		{
			input.LoginID = this.session.LoginID;
			await this.authenticateService.ChangePassword(input);
		}

		/// <summary>
		/// The CurrentLoginAsync.
		/// </summary>
		/// <returns>The <see cref="Task{AuthenticateResponse}"/>.</returns>
		[HttpGet]
		[ActionName("current")]
		[Authorize]
		public async Task<AuthenticateResponse> CurrentLoginAsync()
		{
			var response = await this.authenticateService.GetCurrentLogin(this.session.LoginID.Value);

			if (response == null)
			{
				throw new UnauthorizedAccessException("Invalid user");
			}

			//response.Token = this.
			response.Type = response.User != null ? "User" : "Store";
			response.Token = response.User != null ? this.GenerateJSONWebToken(response.User) : this.GenerateJSONWebToken(response.Store);

			return response;
		}

		/// <summary>
		/// The GenerateJSONWebToken.
		/// </summary>
		/// <param name="claims">The claims<see cref="Claim[]"/>.</param>
		/// <returns>The <see cref="string"/>.</returns>
		private string GenerateJSONWebToken(Claim[] claims)
		{
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["Jwt:Key"]));
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				this.configuration["Jwt:Issuer"],
				this.configuration["Jwt:Issuer"],
				claims,
				expires: DateTime.Now.AddMinutes(120),
				signingCredentials: credentials);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}

		/// <summary>
		/// The GenerateJSONWebToken.
		/// </summary>
		/// <param name="store">The store<see cref="Store"/>.</param>
		/// <returns>The <see cref="string"/>.</returns>
		private string GenerateJSONWebToken(Store store)
		{
			var claims = new[] {
						new Claim(ClaimTypes.NameIdentifier, store.LoginID.ToString()),
						new Claim("StoreID", store.ID.ToString()),
						new Claim(ClaimTypes.Name, store.Code),
						new Claim(JwtRegisteredClaimNames.Typ, "Store"),
						new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) };

			return this.GenerateJSONWebToken(claims);
		}

		/// <summary>
		/// The GenerateJSONWebToken.
		/// </summary>
		/// <param name="user">The user<see cref="User"/>.</param>
		/// <returns>The <see cref="string"/>.</returns>
		private string GenerateJSONWebToken(User user)
		{
			var claims = new[] {
						new Claim(ClaimTypes.NameIdentifier, user.LoginID.ToString()),
						new Claim("UserID", user.ID.ToString()),
						new Claim(ClaimTypes.GivenName, user.FirstName),
						new Claim(ClaimTypes.Surname, user.LastName),
						new Claim(ClaimTypes.Name, user.LoginName),
						new Claim(ClaimTypes.Email, user.Email),
						new Claim(ClaimTypes.Role, user.Type.ToString()),
						new Claim(JwtRegisteredClaimNames.Typ, "User"),
						new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) };

			return this.GenerateJSONWebToken(claims);
		}
	}
}
