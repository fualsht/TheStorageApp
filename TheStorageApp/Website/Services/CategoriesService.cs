using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TheStorageApp.Shared.Models;
using TheStorageAppWebsite.Utils;

namespace TheStorageApp.Website.Services
{
    public class CategoriesService
    {
        public Category[] Categories { get; set; }
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _contextFactory;
        private readonly HttpContextCookieController _httpContextCookieController;

        public CategoriesService(IHttpClientFactory httpClient, IHttpContextAccessor contextFactory, HttpContextCookieController httpContextCookieController)
        {
            _httpClientFactory = httpClient;
            _contextFactory = contextFactory;
            _httpContextCookieController = httpContextCookieController;
            Categories = null;
        }

        public async Task GetCategoryAsync()
        {
            string token = _httpContextCookieController.Get("token");

            Categories = null;
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get, "/api/Categories/GetCategories");

            var client = _httpClientFactory.CreateClient("TGSClient");
            
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.SendAsync(httpRequest);

            if (response.IsSuccessStatusCode)
            {
                Categories = await response.Content.ReadFromJsonAsync<Category[]>();
            }
        }

        public async Task<Category> AddCategoryAsync(Category category)
        {
            var client = _httpClientFactory.CreateClient("TGSClient");
            var responce = await client.PostAsJsonAsync<Category>("/api/Categories/AddCategory", category);
            var newCateory = await responce.Content.ReadFromJsonAsync<Category>();
            
            return newCateory;
        }

        public async Task<Category> UpdateCategoryAsync(Category category)
        {
            var client = _httpClientFactory.CreateClient("TGSClient");
            var responce = await client.PutAsJsonAsync<Category>($"/api/Categories/UpdateCategory/{category.Id.ToString()}", category);
            var updatedCateory = await responce.Content.ReadFromJsonAsync<Category>();

            return updatedCateory;
        }

        public async Task<Category[]> DeleteCategoryAsync()
        {
            var client = _httpClientFactory.CreateClient("TGSClient");
            var toDelete = Categories.Where(x => x.IsSelected).ToArray();

            int s = 0;
            if (toDelete.Count() >= Categories.Count())
                s = toDelete.Count() - 1;

            for (int i = 0; i < toDelete.Count() - s; i++)
            {
                var responce = await client.DeleteAsync($"/api/Categories/DeleteCategory/{toDelete[i].Id.ToString()}");
            }
            return toDelete;
        }

        public void Select(Category category)
        {
            foreach (var item in Categories)
            {
                item.IsSelected = false;
            }
            category.IsSelected = true;
        }

        public void ToggleSelect(Category category)
        {
            category.IsSelected = !category.IsSelected;
        }
        public void SelectRange(int start, int end)
        {
            foreach (var item in Categories)
            {
                item.IsSelected = false;
            }
            for (int i = start; i < end; i++)
            {
                Categories[i].IsSelected = true;
            }
        }
    }
}
