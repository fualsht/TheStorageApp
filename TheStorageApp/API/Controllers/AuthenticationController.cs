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
using System.Security.Claims;
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
        public async Task<IActionResult> LogIn([FromBody] AppUser user)
        {
            try
            {
                AppUser result = await _userManager.FindByNameAsync(user.UserName);

                if (result != null)
                {
                    var signInResult = await _signInManager.PasswordSignInAsync(result, user.Password, true, false);
                    if (signInResult.Succeeded)
                    {
                        var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(result);

                        var expireDate = DateTime.Now.AddMinutes(120);

                        var tokenString = GenerateJWT(claimsPrincipal, expireDate);

                        return Ok(new { token = tokenString, expire = expireDate });
                    }
                    else
                        return Unauthorized();
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return this.InternalServerError(ex, "Fatal error logging in user: " + user.UserName);
            }

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
                return this.InternalServerError(exception, "Error registering user: " + user.UserName);
            }
            
        }

        [HttpGet]
        [Route("LogOut")]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }


        private string GenerateJWT(ClaimsPrincipal claimsPrincipal, DateTime expiry)
        {
            var issuer = _configuration["JWTToken:Issuer"];
            var audience = _configuration["JWTToken:Audience"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTToken:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            
            var token = new JwtSecurityToken(issuer: issuer , audience: audience, claimsPrincipal.Claims, expires: expiry, signingCredentials: credentials);

            var tokenHandler = new JwtSecurityTokenHandler();
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;
        }
    }
}
