// <copyright file="UserService.cs" company="Ayvan">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>UTKARSHLAPTOP\Utkarsh</author>
// <date>2020-02-28</date>

namespace MammothAPI.Services
{
	using Microsoft.EntityFrameworkCore;
	using MammothAPI.Common;
	using MammothAPI.Models;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	/// <summary>
	/// Defines the <see cref="UserService" />
	/// </summary>
	public class UserService : IUsersService
	{
		/// <summary>
		/// Defines the encryptionHelper
		/// </summary>
		private readonly IEncryptionHelper encryptionHelper;

		/// <summary>
		/// Defines the session
		/// </summary>
		private readonly ISession session;

		/// <summary>
		/// Initializes a new instance of the <see cref="UserService"/> class.
		/// </summary>
		/// <param name="encryptionHelper">The encryptionHelper<see cref="IEncryptionHelper"/></param>
		/// <param name="session">The session<see cref="ISession"/></param>
		public UserService(
			IEncryptionHelper encryptionHelper,
			ISession session)
		{
			this.encryptionHelper = encryptionHelper;
			this.session = session;
		}

		/// <inheritdoc />
		public async Task<User> Authenticate(string email, string password)
		{
			//var encPassword = this.encryptionHelper.Encrypt(password);
			//var user = await this.myFortDBContext.Users
			//	.Where(x => x.Email.ToLower() == email.ToLower() && x.Password == encPassword)
			//	.FirstOrDefaultAsync<MammothAPI.Data.Users>();

			//if (user != null)
			//{
			//	return this.MapUser(user);
			//}
			//else
			//{
				return null;
			//}
		}

		/// <inheritdoc />
		public async Task<User> GetUser(int id)
		{
			//var user = await this.myFortDBContext.Users.FirstOrDefaultAsync<MammothAPI.Data.Users>(x => x.Id == id);
			//if (user != null)
			//{
			//	return this.MapUser(user);
			//}
			//else
			//{
				return null;
			//}
		}

		/// <summary>
		/// The GetUsers
		/// </summary>
		/// <returns>The <see cref="Task{List{User}}"/></returns>
		public async Task<List<User>> GetUsers()
		{
			//var users = await this.myFortDBContext.Users.ToListAsync();
			//return users.Select(x => MapUser(x)).ToList();

			return null;
		}

		/// <inheritdoc />
		public async Task<User> RegisterUser(RegisterUser registerUser)
		{
			//var existingUser = await this.myFortDBContext.Users.FirstOrDefaultAsync<MammothAPI.Data.Users>(x => x.Email == registerUser.User.Email);
			//if (existingUser != null)
			//{
			//	throw new Exception("User with current email already exists, try login using your email and password.");
			//}

			//var user = new Users
			//{
			//	Email = registerUser.User.Email,
			//	FirstName = registerUser.User.FirstName,
			//	LastName = registerUser.User.LastName,
			//	IsActive = false,
			//	Password = this.encryptionHelper.Encrypt(registerUser.Password),
			//	Type = (int)Models.TypeOfUser.RegularUser,
			//	LastModifiedOn = DateTime.Now,
			//	LastModifiedBy = 1 //// Default user
			//};

			//this.myFortDBContext.Users.Add(user);
			//await this.myFortDBContext.SaveChangesAsync();

			//return this.MapUser(user);
			return null;
		}

		/// <inheritdoc />
		public async Task<User> UpdateUser(User user)
		{
			//var existingUser = await this.myFortDBContext.Users.FirstOrDefaultAsync<MammothAPI.Data.Users>(x => x.Id == user.ID);
			//if (existingUser != null)
			//{
			//	existingUser.FirstName = user.FirstName ?? existingUser.FirstName;
			//	existingUser.LastName = user.LastName ?? existingUser.LastName;
			//	existingUser.Email = user.Email ?? existingUser.Email;
			//	existingUser.IsActive = user.IsActive ?? existingUser.IsActive;
			//	existingUser.Type = user.Type.HasValue ? (int)user.Type.Value : existingUser.Type;
			//	existingUser.LastModifiedOn = DateTime.Now;
			//	existingUser.LastModifiedBy = this.session.UserID.Value;

			//	this.myFortDBContext.Update(existingUser);
			//	this.myFortDBContext.SaveChanges();

			//	return this.MapUser(existingUser);
			//}
			//else
			//{
			//	return null;
			//}
			return null;
		}

		/// <summary>
		/// The MapUser
		/// </summary>
		/// <param name="user">The user<see cref="Users"/></param>
		/// <returns>The <see cref="User"/></returns>
		//private User MapUser(Users user)
		//{
		//	//return new User
		//	//{
		//	//	ID = user.Id,
		//	//	Email = user.Email,
		//	//	FirstName = user.FirstName,
		//	//	LastName = user.LastName,
		//	//	IsActive = user.IsActive,
		//	//	Type = (Models.TypeOfUser)user.Type,
		//	//};
		//	return null;
		//}
	}
}
