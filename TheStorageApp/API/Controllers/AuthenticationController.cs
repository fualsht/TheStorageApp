using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TheStorageApp.API.Models;
using TheStorageApp.Shared.Models;

namespace TheStorageApp.API.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthenticationController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IConfiguration configuration)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> LogIn([FromBody]object userjson)
        {
            string j = "";
            //User u = JsonSerializer.Deserialize<User>(userjson);
            string s = "";
            //AppUser user = await _userManager.FindByNameAsync(_user.UserName);

            //if (user != null)
            //{
                //var signInResult = await _signInManager.PasswordSignInAsync(user, _user.Password, false, false);
                //if (signInResult.Succeeded)
                //    return RedirectToAction("Index");
                //else 
            //        return RedirectToAction("LogIn");
            //}
            //else
                return RedirectToAction("LogIn");
        }

        [HttpPost]
        public async Task<IActionResult> Register(
            string username, 
            string firstname,
            string lastname,
            string email, 
            string password)
        {
            
            var user = new AppUser()
            {
                UserName = username,
                FirstName = firstname,
                LastName = lastname,
                Email = email
            };

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(user, password, false, false);
                if (signInResult.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }


        private string GenerateJWT()
        {
            var issuer = _configuration["JWTToken:Issuer"];
            var audience = _configuration["JWTToken:Audience"];
            var expiry = DateTime.Now.AddMinutes(120);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTToken:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(issuer: issuer, audience: audience, expires: expiry, signingCredentials: credentials);

            var tokenHandler = new JwtSecurityTokenHandler();
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;
        }
    }
}
