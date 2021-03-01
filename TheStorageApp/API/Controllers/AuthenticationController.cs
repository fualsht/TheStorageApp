using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TheStorageApp.API.Data;
using TheStorageApp.API.Models;

namespace TheStorageApp.API.Controllers
{
    [Route("api/Authentication")]
    public class AuthenticationController : APIControllerBase<AppUser>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthenticationController(ILogger<AuthenticationController> logger,
            DataContext dataContext, IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager, IConfiguration configuration)
            : base(logger, dataContext, httpContextAccessor)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("LogIn")]
        public async Task<IActionResult> LogIn([FromBody]AppUser userjson)
        {
            AppUser user = await _userManager.FindByNameAsync(userjson.UserName);

            if (user != null)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(user, userjson.Password, false, false);
                if (signInResult.Succeeded)
                {
                    var tokenString = GenerateJWT();
                    return Ok(new { token = tokenString, userid = user.Id });
                }
                else
                    return Unauthorized();
            }
            else
                return Unauthorized();
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody]AppUser user)
        {
            try
            {
                var result = await _userManager.CreateAsync(user, user.Password);

                if (result.Succeeded)
                {
                    var signInResult = await _signInManager.PasswordSignInAsync(user, user.Password, false, false);
                    if (signInResult.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                return this.InternalServerError(exception, "Error registering user");
            }
            
        }

        [HttpGet]
        [Route("LogOut")]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return Ok();
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
