using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json; // for HttpClientJsonExtensions
using System.Threading.Tasks;
using TheStorageApp.Shared.Models;
using TheStorageApp.Website.Utils;

namespace TheStorageApp.Website.Services
{
    public class UsersService : ApiServiceBase<User>
    {
        public User[] Users { get; set; }

        public UsersService(IHttpClientFactory httpClient, IHttpContextAccessor contextFactory, HttpContextCookieController httpContextCookieController) : 
            base(httpClient, contextFactory, httpContextCookieController)
        {

        }

        public async Task GetUsersAsync()
        {
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get, "api/Users/GetUsers");

            var client = _httpClientFactory.CreateClient("TGSClient");
            var response = await client.SendAsync(httpRequest);

            if (response.IsSuccessStatusCode)
            {
                Users = await response.Content.ReadFromJsonAsync<User[]>();
            }
        }

        public async Task<User> AddUserAsync(User user)
        {
            var client = _httpClientFactory.CreateClient("TGSClient");
            var responce = await client.PostAsJsonAsync<User>("/api/Users/AddUser", user);
            var newUser = await responce.Content.ReadFromJsonAsync<User>();

            return newUser;
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            var client = _httpClientFactory.CreateClient("TGSClient");
            var responce = await client.PutAsJsonAsync<User>($"/api/Users/UpdateUser/{user.Id.ToString()}", user);
            var updatedUser = await responce.Content.ReadFromJsonAsync<User>();

            return updatedUser;
        }

        public async Task<User[]> DeleteUsersAsync()
        {
            var userss = Users.ToList<User>();

            var client = _httpClientFactory.CreateClient("TGSClient");
            var toDelete = userss.Where(x => x.IsSelected).ToArray();
            foreach (var item in toDelete)
            {
                var responce = await client.DeleteAsync($"/api/Users/DeleteUser/{item.Id.ToString()}");
                userss.Remove(item);
            }
            Users = userss.ToArray();
            return toDelete;
        }

        public void Select(User user)
        {
            foreach (var item in Users)
            {
                item.IsSelected = false;
            }
            user.IsSelected = true;
        }

        public void ToggleSelect(User user)
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
