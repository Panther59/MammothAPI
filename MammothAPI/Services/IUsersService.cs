// <copyright file="IUsersService.cs" company="Ayvan">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>UTKARSHLAPTOP\Utkarsh</author>
// <date>2020-02-27</date>

namespace MammothAPI.Services
{
	using MammothAPI.Models;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	/// <summary>
	/// Defines the <see cref="IUsersService" />
	/// </summary>
	public interface IUsersService
	{
		/// <summary>
		/// The Authenticate
		/// </summary>
		/// <param name="email">The email<see cref="string"/></param>
		/// <param name="password">The password<see cref="string"/></param>
		/// <returns>The <see cref="Task{User}"/></returns>
		Task<User> Authenticate(string email, string password);

		/// <summary>
		/// The GetUser
		/// </summary>
		/// <param name="id">The id<see cref="int"/></param>
		/// <returns>The <see cref="Task{User}"/></returns>
		Task<User> GetUser(int id);

		/// <summary>
		/// The GetUsers
		/// </summary>
		/// <returns>The <see cref="Task{List{User}}"/></returns>
		Task<List<User>> GetUsers();

		/// <summary>
		/// The RegisterUser
		/// </summary>
		/// <param name="registerUser">The registerUser<see cref="RegisterUser"/></param>
		/// <returns>The <see cref="Task{User}"/></returns>
		Task<User> RegisterUser(RegisterUser registerUser);

		/// <summary>
		/// The UpdateUser
		/// </summary>
		/// <param name="user">The user<see cref="User"/></param>
		/// <returns>The <see cref="Task{User}"/></returns>
		Task<User> UpdateUser(User user);
	}
}
