using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TheStorageApp.Shared.Models;

namespace TheStorageApp.Website.Services
{
    public class CategoriesService
    {
        public Category[] Categories { get; set; }

        private readonly IHttpClientFactory _httpClientFactory;

        public CategoriesService(IHttpClientFactory httpClient)
        {
            _httpClientFactory = httpClient;
            Categories = new Category[0];
        }

        public async Task GetCategoryAsync()
        {
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get, "/api/Categories/GetCategories");

            var client = _httpClientFactory.CreateClient("TGSClient");
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
            foreach (var item in toDelete)
            {
                var responce = await client.DeleteAsync($"/api/Receipts/DeleteCategory/{item.Id.ToString()}");
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
