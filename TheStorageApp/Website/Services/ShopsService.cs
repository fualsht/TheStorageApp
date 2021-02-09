using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TheStorageApp.Shared.Models;

namespace TheStorageApp.Website.Services
{
    public class ShopsService
    {
        public Shop[] Shops { get; set; }

        private readonly IHttpClientFactory _httpClientFactory;

        public ShopsService(IHttpClientFactory httpClient)
        {
            _httpClientFactory = httpClient;
            Shops = new Shop[0];
        }

        public async Task GetShopsAsync()
        {
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get, "/api/Shops/GetShops");

            var client = _httpClientFactory.CreateClient("TGSClient");
            var response = await client.SendAsync(httpRequest);

            if (response.IsSuccessStatusCode)
            {
                Shops = await response.Content.ReadFromJsonAsync<Shop[]>();
            }
        }

        public async Task<Shop> AddShopAsync(Shop shop)
        {
            var client = _httpClientFactory.CreateClient("TGSClient");
            var responce = await client.PostAsJsonAsync<Shop>("/api/Shops/AddShop", shop);
            var newShop = await responce.Content.ReadFromJsonAsync<Shop>();
            
            return newShop;
        }

        public async Task<Shop> UpdateShopAsync(Shop shop)
        {
            var client = _httpClientFactory.CreateClient("TGSClient");
            var responce = await client.PutAsJsonAsync<Shop>($"/api/Shops/UpdateShop/{shop.Id.ToString()}", shop);
            var updatedShop = await responce.Content.ReadFromJsonAsync<Shop>();

            return updatedShop;
        }

        public async Task<Shop[]> DeleteShopAsync()
        {
            var client = _httpClientFactory.CreateClient("TGSClient");
            var toDelete = Shops.Where(x => x.IsSelected).ToArray();
            foreach (var item in toDelete)
            {
                var responce = await client.DeleteAsync($"/api/Receipts/DeleteShops/{item.Id.ToString()}");
            }
            return toDelete;
        }

        public void Select(Shop shop)
        {
            foreach (var item in Shops)
            {
                item.IsSelected = false;
            }
            shop.IsSelected = true;
        }

        public void ToggleSelect(Shop shop)
        {
            shop.IsSelected = !shop.IsSelected;
        }
        public void SelectRange(int start, int end)
        {
            foreach (var item in Shops)
            {
                item.IsSelected = false;
            }
            for (int i = start; i < end; i++)
            {
                Shops[i].IsSelected = true;
            }
        }
    }
}
