// <copyright file="AuthenticateService.cs" company="Ayvan">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>UTKARSHLAPTOP\Utkarsh</author>
// <date>2020-03-01</date>

namespace MammothAPI.Services
{
	using MammothAPI.Models;
	using System.Threading.Tasks;

	/// <summary>
	/// Defines the <see cref="IAuthenticateService" />
	/// </summary>
	public interface IAuthenticateService
	{
		/// <summary>
		/// The AuthenticateStore
		/// </summary>
		/// <param name="request">The request<see cref="AuthenticateRequest"/></param>
		/// <returns>The <see cref="Task{Store}"/></returns>
		Task<Store> AuthenticateStore(AuthenticateRequest request);

		/// <summary>
		/// The AuthenticateUser
		/// </summary>
		/// <param name="request">The request<see cref="AuthenticateRequest"/></param>
		/// <returns>The <see cref="Task{User}"/></returns>
		Task<User> AuthenticateUser(AuthenticateRequest request);

		/// <summary>
		/// The GetCurrentLogin
		/// </summary>
		/// <param name="loginId">The loginId<see cref="int"/></param>
		/// <returns>The <see cref="Task{AuthenticateResponse}"/></returns>
		Task<AuthenticateResponse> GetCurrentLogin(int loginId);
	}
}
