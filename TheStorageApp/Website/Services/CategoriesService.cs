using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TheStorageApp.Shared.Models;
using TheStorageAppWebsite.Services;
using TheStorageAppWebsite.Utils;

namespace TheStorageApp.Website.Services
{
    public class CategoriesService : ApiServiceBase<Category>
    {
        public Category[] Categories { get; set; }

        public CategoriesService(IHttpClientFactory httpClient, IHttpContextAccessor contextFactory, HttpContextCookieController httpContextCookieController):
            base(httpClient, contextFactory, httpContextCookieController)
        {
            Categories = null;
        }

        public async Task GetCategoryAsync()
        {
            Categories = null;

            var response = await ApiGet("/api/Categories/GetCategories");

            if (response.IsSuccessStatusCode)
            {
                Categories = await response.Content.ReadFromJsonAsync<Category[]>();
            }
        }

        public async Task<Category> AddCategoryAsync(Category category)
        {
            var responce = await ApiPost("/api/Categories/AddCategory", category);

            if (responce.IsSuccessStatusCode)
            {
                var newitem = await responce.Content.ReadFromJsonAsync<Category>();
                return newitem;
            }
            else
                return null;
        }

        public async Task<Category> UpdateCategoryAsync(Category category)
        {
            string url = "/api/Categories/UpdateCategory";
            var responce = await ApiUpdate(url, category);

            if (responce.IsSuccessStatusCode)
            {
                var newitem = await responce.Content.ReadFromJsonAsync<Category>();
                return newitem;
            }
            else
                return null;
        }

        public async Task<bool> DeleteCategoryAsync(string id)
        {
            var responce = await ApiDelete($"/api/Categories/DeleteCategory{id}");

            if (responce.IsSuccessStatusCode)
                return true;
            else
                return false;
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
