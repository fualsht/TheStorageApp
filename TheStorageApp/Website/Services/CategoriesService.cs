using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TheStorageApp.Website.Models;
using TheStorageApp.Website.Utils;
using TheStorageApp.Website.Services;
using System;

namespace TheStorageApp.Website.Services
{
    public class CategoriesService : WebServiceBase<Category>
    {
        public Category[] Categories { get; set; }

        public CategoriesService(IHttpClientFactory httpClient, IHttpContextAccessor contextFactory, CookieController httpContextCookieController):
            base(httpClient, contextFactory, httpContextCookieController)
        {
            Categories = null;
        }

        public async Task GetCategoryAsync()
        {
            try
            {
                Categories = null;

                var response = await ApiGet("/api/Categories/GetCategories");

                if (response.IsSuccessStatusCode)
                {
                    Categories = await response.Content.ReadFromJsonAsync<Category[]>();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public async Task<Category> AddCategoryAsync(Category category)
        {
            var responce = await ApiPost("/api/Categories/AddCategory", category);

            if (responce.IsSuccessStatusCode)
            {
                var v = responce.Headers.FirstOrDefault(x => x.Key == "user");
                var newitem = await responce.Content.ReadFromJsonAsync<Category>();
                return newitem;
            }
            else
                return null;
        }

        public async Task<Category> UpdateCategoryAsync(Category category)
        {
            var responce = await ApiUpdate("/api/Categories/UpdateCategory", category);

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
            var responce = await ApiDelete($"/api/Categories/DeleteCategory", id);

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
