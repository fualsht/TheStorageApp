using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using TheStorageApp.Website.Models;
using TheStorageApp.Website.Utils;

namespace TheStorageApp.Website.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly IHttpClientFactory _httpContextFactory;
        private readonly CookieController _httpContextCookieController;

        public AuthorizationController(IHttpClientFactory httpContextFactory, CookieController httpContextCookieController)
        {
            _httpContextFactory = httpContextFactory;
            _httpContextCookieController = httpContextCookieController;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(string submitbutton, string username, string password)
        {
            HttpClient client = _httpContextFactory.CreateClient("TGSClient");

            AppUser userModel = new AppUser();
            userModel.UserName = username;
            userModel.Password = password;

            HttpResponseMessage response = await client.PostAsJsonAsync<AppUser>("api/Authorization/LogIn", userModel);
            if (response.IsSuccessStatusCode)
            {
                JWTToken jwt = await response.Content.ReadFromJsonAsync<JWTToken>();
                System.IdentityModel.Tokens.Jwt.JwtSecurityToken jwtSecurityToken = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(jwt.Token);
                HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(jwtSecurityToken.Claims));

                _httpContextCookieController.Set("token", jwt.Token, jwt.Expire);

                return Redirect("~/");
            }
            else
            {
                return View();
            }
        }

        public async Task<IActionResult> LogOut()
        {
            HttpClient client = _httpContextFactory.CreateClient("TGSClient");

            HttpResponseMessage response = await client.GetAsync("api/Authorization/LogOut");
            if (response.IsSuccessStatusCode)
            {
                _httpContextCookieController.Delete("token");
                return RedirectToAction("Index");
            }
            else
                return Ok();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string username, string firstname, string lastname, string email, string password)
        {
            AppUser user = new AppUser();
            user.UserName = username;
            user.FirstName = firstname;
            user.LastName = lastname;
            user.Password = password;
            user.Email = email;

            HttpClient client = _httpContextFactory.CreateClient("TGSClient");
            var responce = await client.PostAsJsonAsync<AppUser>("api/Authorization/Register", user);

            if (responce.IsSuccessStatusCode)
            {
                return RedirectToAction("Register");
            }

            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword()
        {
            return Ok();
        }

        public class JWTToken
        {
            public string Token { get; set; }


            public DateTime Expire { get; set; }
            
           
        }
    }
}
