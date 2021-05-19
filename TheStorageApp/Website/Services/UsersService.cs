using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json; // for HttpClientJsonExtensions
using System.Threading.Tasks;
using TheStorageApp.Website.Models;
using TheStorageApp.Website.Utils;

namespace TheStorageApp.Website.Services
{
    public class UsersService : WebServiceBase<AppUser>
    {
        public List<AppUser> Users { get; set; }

        public UsersService(IHttpClientFactory httpClient, IHttpContextAccessor contextFactory, CookieController httpContextCookieController) : 
            base(httpClient, contextFactory, httpContextCookieController)
        {

        }

        public async Task GetUsersAsync()
        {
            var responce = await this.ApiGet("api/Users/GetUsers");
            Users = new List<AppUser>();

            if (responce.IsSuccessStatusCode)
            {
                var users = await responce.Content.ReadFromJsonAsync<AppUser[]>();
                Users = users.ToList();
            }
        }

        public async Task<AppUser> AddUserAsync(AppUser user)
        {
            var client = _httpClientFactory.CreateClient("TGSClient");
            var responce = await client.PostAsJsonAsync<AppUser>("/api/Users/AddUser", user);
            var newUser = await responce.Content.ReadFromJsonAsync<AppUser>();

            return newUser;
        }

        public async Task<AppUser> UpdateUserAsync(AppUser user)
        {
            var client = _httpClientFactory.CreateClient("TGSClient");
            var responce = await client.PutAsJsonAsync<AppUser>($"/api/Users/UpdateUser/{user.Id}", user);
            var updatedUser = await responce.Content.ReadFromJsonAsync<AppUser>();

            return updatedUser;
        }

        public async Task<AppUser[]> DeleteUsersAsync()
        {
            var userss = Users.ToList<AppUser>();

            var client = _httpClientFactory.CreateClient("TGSClient");
            var toDelete = userss.Where(x => x.IsSelected).ToArray();
            foreach (var item in toDelete)
            {
                var responce = await client.DeleteAsync($"/api/Users/DeleteUser/{item.Id.ToString()}");
                userss.Remove(item);
            }
            Users = userss;
            return toDelete;
        }

        public void Select(AppUser user)
        {
            foreach (var item in Users)
            {
                item.IsSelected = false;
            }
            user.IsSelected = true;
        }

        public void ToggleSelect(AppUser user)
        {
            user.IsSelected = !user.IsSelected;
        }
        public void SelectRange(int start, int end)
        {
            foreach (var item in Users)
            {
                item.IsSelected = false;
            }
            for (int i = start; i < end; i++)
            {
                Users[i].IsSelected = true;
            }
        }
    }
}
