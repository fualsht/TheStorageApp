using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheStorageApp.API.Data;
using TheStorageApp.API.Models;

namespace TheGrocerWebApi.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly DataContext _context;

        public UserController(ILogger<UserController> logger, DataContext dataContext)
        {
            _logger = logger;
            _context = dataContext;
        }

        [HttpGet]
        [Route("GetUsers")]
        public async Task<AppUser[]> GetUsers()
        {
            return await _context.Users.ToArrayAsync();
        }

        [HttpGet]
        [Route("GetUser/{id}")]
        public async Task<AppUser> GetUser(string id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id.ToString() == id);
        }

        [HttpGet]
        [Route("GetUserByName/{name}")]
        public async Task<AppUser> GetUserByName(string name)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.UserName == name);
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
                LastName = user.LastName
            };

            await _context.Users.AddAsync(newuser);
            await _context.SaveChangesAsync();
            return newuser;// JsonSerializer.Serialize<User>(user);
        }

        [HttpPut]
        [Route("UpdateUser/{id}")]
        public async Task<AppUser> UpdateUser(string id, [FromBody]AppUser user)
        {
            AppUser currentUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (currentUser != null)
            {
                if (!string.IsNullOrEmpty(user.Email))
                    currentUser.Email = user.Email;

                if (!string.IsNullOrEmpty(user.FirstName))
                    currentUser.FirstName = user.FirstName;

                if (!string.IsNullOrEmpty(user.LastName))
                    currentUser.LastName = user.LastName;

                _context.Update(currentUser);
                await _context.SaveChangesAsync();
            }

            return currentUser;
        }

        [HttpDelete]
        [Route("DeleteUser/{id}")]
        public async Task<AppUser> DeleteUser(string id)
        {
            var userToDelete = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            _context.Users.Remove(userToDelete);
            await _context.SaveChangesAsync();
            return userToDelete;
        }
    }
}
