using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TheStorageApp.Shared.Models;
using TheStorageApp.Website.Utils;

namespace TheStorageApp.Website.Services
{
    public class ShopsService : ApiServiceBase<Shop>
    {
        protected const string getEndpoint = "/api/Shops/GetShop";
        protected const string addSingleEndpoint = "/api/Shops/AddShop";
        protected const string addMultipleEndpoint = "/api/Shops/AddShops";
        protected const string updateSingleEndpoint = "/api/Shops/UpdateShop";
        protected const string updateMultipleEndpoint = "/api/Shops/UpdateShops";
        protected const string DeleteSingleEndpoint = "/api/Receipts/DeleteShop";
        protected const string DeleteMultipleEndpoint = "$/api/Receipts/DeleteShops";

        public Shop[] Shops { get; set; }

        public ShopsService(IHttpClientFactory httpClient, IHttpContextAccessor contextFactory, HttpContextCookieController httpContextCookieController) : 
            base(httpClient, contextFactory, httpContextCookieController)
        {
        }

        public async Task<Shop> GetShopsAsync()
        {
            Shop returnShop = null;

            var response = await ApiGet(getEndpoint);

            if (response.IsSuccessStatusCode)
            {
                returnShop = await response.Content.ReadFromJsonAsync<Shop>();
            }

            return returnShop;
        }

        public async Task<Shop> GetShopAsync(Shop shop)
        {
            Shop returnShop = null;

            var response = await ApiGet(getEndpoint);

            if (response.IsSuccessStatusCode)
            {
                returnShop = await response.Content.ReadFromJsonAsync<Shop>();
            }

            return returnShop;
        }

        public async Task<Shop[]> GetShopAsync(Shop[] shops)
        {
            Shop[] returnShops = null;

            List<string> ids = new List<string>();

            foreach (var item in shops)
                ids.Add(item.Id.ToString());

            var response = await ApiGet(getEndpoint, ids.ToArray());

            if (response.IsSuccessStatusCode)
            {
                returnShops = await response.Content.ReadFromJsonAsync<Shop[]>();
            }

            return returnShops;
        }

        public async Task<Shop> AddShopAsync(Shop shop)
        {
            var response = await ApiPost(addSingleEndpoint, shop);

            Shop newShop = null;

            if (response.IsSuccessStatusCode)
            {
                newShop = await response.Content.ReadFromJsonAsync<Shop>();
            }            
            
            return newShop;
        }

        public async Task<Shop[]> AddShopAsync(Shop[] shops)
        {
            var response = await ApiPost(addMultipleEndpoint, shops);

            Shop[] newShops = null;

            if (response.IsSuccessStatusCode)
            {
                newShops = await response.Content.ReadFromJsonAsync<Shop[]>();
            }

            return newShops;
        }

        public async Task<Shop> UpdateShopAsync(Shop shop)
        {
            var response = await ApiUpdate(updateSingleEndpoint, shop);

            Shop updatedShop = null;

            if (response.IsSuccessStatusCode)
            {
                updatedShop = await response.Content.ReadFromJsonAsync<Shop>();
            } 

            return updatedShop;
        }

        public async Task<Shop[]> UpdateShopAsync(Shop[] shop)
        {
            var response = await ApiUpdate(updateMultipleEndpoint, shop);

            Shop[] updatedShops = null;

            if (response.IsSuccessStatusCode)
            {
                updatedShops = await response.Content.ReadFromJsonAsync<Shop[]>();
            }

            return updatedShops;
        }

        public async Task<Shop> DeleteShopAsync(Shop shop)
        {
            var response = await ApiDelete(DeleteSingleEndpoint, shop.Id.ToString());

            Shop deletedShop = null;

            if (response.IsSuccessStatusCode)
            {
                deletedShop = await response.Content.ReadFromJsonAsync<Shop>();
            }

            return deletedShop;
        }

        public async Task<Shop[]> DeleteShopAsync(Shop[] shops)
        {
            List<string> ids = new List<string>();

            foreach (Shop shop in shops)
                ids.Add(shop.Id.ToString());

            var response = await ApiDelete(DeleteMultipleEndpoint, ids.ToArray());

            Shop[] deletedShop = null;

            if (response.IsSuccessStatusCode)
            {
                deletedShop = await response.Content.ReadFromJsonAsync<Shop[]>();
            }

            return deletedShop;
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
