using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TheStorageApp.Shared.Models;
using TheStorageApp.Website.Models;

namespace TheStorageAppWebsite.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly IHttpClientFactory _httpContextFactory;

        public AuthorizationController(IHttpClientFactory httpContextFactory)
        {
            _httpContextFactory = httpContextFactory;
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
        public async Task<IActionResult> LogIn(string username, string firstname, string lastname, string email, string password)
        {
            HttpClient client = _httpContextFactory.CreateClient("TGSClient");

            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);

            AppUser userModel = new AppUser();
            userModel.UserName = username;
            userModel.Password = password;

            HttpResponseMessage response = await client.PostAsJsonAsync<AppUser>("/Authentication/LogIn", userModel);
            JWTToken jwt = await response.Content.ReadFromJsonAsync<JWTToken>();

            HttpContext.Session.SetString("token", jwt.Token);

            ViewBag.Message = "User logged in successfully!";

            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string username, string firstname, string lastname, string email, string password)
        {
            return View();
        }

        public class JWTToken
        {
            public string Token { get; set; }
        }
    }
}
