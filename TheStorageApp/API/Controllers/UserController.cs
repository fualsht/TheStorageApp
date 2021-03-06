﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheStorageApp.API.Controllers;
using TheStorageApp.API.Data;
using TheStorageApp.API.Models;

namespace TheGrocerWebApi.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class UserController : APIControllerBase<AppUser>
    {
        public UserController(ILogger<APIControllerBase<AppUser>> logger, DataContext dataContext, IHttpContextAccessor httpContextAccessor) : base(logger, dataContext, httpContextAccessor)
        {
        }

        [HttpGet]
        [Route("GetUsers")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<AppUser[]>> GetUsers()
        {
            try
            {
                var users = _dataContext.Users;
                var usersarray = await users.ToArrayAsync();
                return Ok(usersarray);
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        [HttpGet]
        [Route("GetUser/{id}")]
        public async Task<AppUser> GetUser(string id)
        {
            return await _dataContext.Users.FirstOrDefaultAsync(x => x.Id.ToString() == id);
        }

        [HttpGet]
        [Route("GetUserByName/{name}")]
        public async Task<AppUser> GetUserByName(string name)
        {
            return await _dataContext.Users.SingleOrDefaultAsync(x => x.UserName == name);
        }

        [HttpPost]
        [Route("AddUser")]
        public async Task<AppUser> AddUser([FromBody] AppUser user)
        {
            //User user = JsonSerializer.Deserialize<User>(userJson);
            AppUser newuser = new AppUser()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserRoles = user.UserRoles
            };

            await _dataContext.Users.AddAsync(newuser);
            await _dataContext.SaveChangesAsync();
            return newuser;// JsonSerializer.Serialize<User>(user);
        }

        [HttpPut]
        [Route("UpdateUser/{id}")]
        public async Task<AppUser> UpdateUser([FromBody]AppUser user)
        {
            AppUser currentUser = await _dataContext.Users.FirstOrDefaultAsync(x => x.Id == user.Id);

            if (currentUser != null)
            {
                if (!string.IsNullOrEmpty(user.Email))
                    currentUser.Email = user.Email;

                if (!string.IsNullOrEmpty(user.FirstName))
                    currentUser.FirstName = user.FirstName;

                if (!string.IsNullOrEmpty(user.LastName))
                    currentUser.LastName = user.LastName;

                // ToDo: Add, Delete and Update roles
                currentUser.UserRoles = user.UserRoles;

                _dataContext.Update(currentUser);
                await _dataContext.SaveChangesAsync();
            }

            return currentUser;
        }

        [HttpDelete]
        [Route("DeleteUser/{id}")]
        public async Task<AppUser> DeleteUser(string id)
        {
            var userToDelete = await _dataContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            _dataContext.Users.Remove(userToDelete);
            await _dataContext.SaveChangesAsync();
            return userToDelete;
        }
    }
}
