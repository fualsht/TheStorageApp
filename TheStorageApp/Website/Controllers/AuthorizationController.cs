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
using TheStorageAppWebsite.Utils;

namespace TheStorageAppWebsite.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly IHttpClientFactory _httpContextFactory;
        private readonly HttpContextCookieController _httpContextCookieController;

        public AuthorizationController(IHttpClientFactory httpContextFactory, HttpContextCookieController httpContextCookieController)
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
        public async Task<IActionResult> LogIn(string username, string firstname, string lastname, string email, string password)
        {
            HttpClient client = _httpContextFactory.CreateClient("TGSClient");

            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);

            AppUser userModel = new AppUser();
            userModel.UserName = username;
            userModel.Password = password;

            HttpResponseMessage response = await client.PostAsJsonAsync<AppUser>("/Authentication/LogIn", userModel);
            if (response.IsSuccessStatusCode)
            {
                JWTToken jwt = await response.Content.ReadFromJsonAsync<JWTToken>();
                _httpContextCookieController.Set("token", jwt.Token, 30);
                HttpContext.Session.SetString("token", jwt.Token);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public async Task<IActionResult> LogOut()
        {
            HttpClient client = _httpContextFactory.CreateClient("TGSClient");

            HttpResponseMessage response = await client.GetAsync("/Authentication/LogOut");
            if (response.IsSuccessStatusCode)
            {
                HttpContext.Session.Remove("token");
                ViewBag.Message = "User logged out successfully!";
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
            return View();
        }

        public class JWTToken
        {
            public string Token { get; set; }
        }
    }
}
