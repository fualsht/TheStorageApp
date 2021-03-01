using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TheStorageApp.Website.Models;
using TheStorageApp.Website.Utils;

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
        public async Task<IActionResult> LogIn(string username, string password)
        {
            HttpClient client = _httpContextFactory.CreateClient("TGSClient");

            //var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            //client.DefaultRequestHeaders.Accept.Add(contentType);

            AppUser userModel = new AppUser();
            userModel.UserName = username;
            userModel.Password = password;

            HttpResponseMessage response = await client.PostAsJsonAsync<AppUser>("api/Authentication/LogIn", userModel);
            if (response.IsSuccessStatusCode)
            {
                JWTToken jwt = await response.Content.ReadFromJsonAsync<JWTToken>();
                _httpContextCookieController.Set("token", jwt.Token, 30);
                _httpContextCookieController.Set("user", jwt.UserId, 30);
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

            HttpResponseMessage response = await client.GetAsync("api/Authentication/LogOut");
            if (response.IsSuccessStatusCode)
            {
                _httpContextCookieController.Delete("token");
                _httpContextCookieController.Delete("user");
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
            var responce = await client.PostAsJsonAsync<AppUser>("api/Authentication/Register", user);

            if (responce.IsSuccessStatusCode)
            {
                return RedirectToAction("Register");
            }

            return View();
        }

        public class JWTToken
        {
            public string Token { get; set; }

            public string UserId { get; set; }
        }
    }
}
