using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheStorageApp.API.Data;
using TheStorageApp.Shared.Models;

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
        public async Task<User[]> GetUsers()
        {
            return await _context.Users.ToArrayAsync();
        }

        [HttpGet]
        [Route("GetUser/{id}")]
        public async Task<User> GetUser(string id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id.ToString() == id);
        }

        [HttpGet]
        [Route("GetUserByName/{name}")]
        public async Task<User> GetUserByName(string name)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.Name == name);
        }

        [HttpPost]
        [Route("AddUser")]
        public async Task<User> AddUser([FromBody]User user)
        {
            string s = "";
            //User user = JsonSerializer.Deserialize<User>(userJson);
            User newuser = new User()
            {
                Id = Guid.NewGuid(),
                Name = user.Name,
                Password = user.Password,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now
            };

            await _context.Users.AddAsync(newuser);
            await _context.SaveChangesAsync();
            return newuser;// JsonSerializer.Serialize<User>(user);
        }

        [HttpPut]
        [Route("UpdateUser/{id}")]
        public async Task<User> UpdateUser(string id, [FromBody]User user)
        {
            User currentUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == new Guid(id));

            if (currentUser != null)
            {
                if (!string.IsNullOrEmpty(user.Name))
                    currentUser.Name = user.Name;

                if (!string.IsNullOrEmpty(user.Password))
                    currentUser.Password = user.Password;

                if (!string.IsNullOrEmpty(user.Email))
                    currentUser.Email = user.Email;

                if (!string.IsNullOrEmpty(user.FirstName))
                    currentUser.FirstName = user.FirstName;

                if (!string.IsNullOrEmpty(user.LastName))
                    currentUser.LastName = user.LastName;

                currentUser.ModifiedOn = DateTime.Now;

                _context.Update(currentUser);
                await _context.SaveChangesAsync();
            }

            return currentUser;
        }

        [HttpDelete]
        [Route("DeleteUser/{id}")]
        public async Task<User> DeleteUser(string id)
        {
            var userToDelete = await _context.Users.FirstOrDefaultAsync(x => x.Id == new Guid(id));
            _context.Users.Remove(userToDelete);
            await _context.SaveChangesAsync();
            return userToDelete;
        }
    }
}
