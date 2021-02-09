using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TheStorageApp.Shared.Models;

namespace TheStorageApp.Website.Services
{
    public class ReceiptsService
    {
        public Receipt[] Receipts { get; set; }

        private readonly IHttpClientFactory _httpClientFactory;

        public ReceiptsService(IHttpClientFactory httpClient)
        {
            _httpClientFactory = httpClient;
            Receipts = new Receipt[0];
        }

        public async Task GetReceiptsAsync()
        {
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get, "/api/Receipts/GetReceipts");

            var client = _httpClientFactory.CreateClient("TGSClient");
            var response = await client.SendAsync(httpRequest);

            if (response.IsSuccessStatusCode)
            {
                Receipts = await response.Content.ReadFromJsonAsync<Receipt[]>();
            }
        }

        public async Task<Receipt> AddReceiptAsync(Receipt receipt)
        {
            var client = _httpClientFactory.CreateClient("TGSClient");
            var responce = await client.PostAsJsonAsync<Receipt>("/api/Receipts/AddReceipt", receipt);
            var newReceipt = await responce.Content.ReadFromJsonAsync<Receipt>();
            
            return newReceipt;
        }

        public async Task<Receipt> UpdateReceiptAsync(Receipt receipt)
        {
            var client = _httpClientFactory.CreateClient("TGSClient");
            var responce = await client.PutAsJsonAsync<Receipt>($"/api/Receipts/GetReceipts/{receipt.Id.ToString()}", receipt);
            var updatedRecipet = await responce.Content.ReadFromJsonAsync<Receipt>();

            return updatedRecipet;
        }

        public async Task<Receipt[]> DeleteReceiptsAsync()
        {
            var receipts = Receipts.ToList<Receipt>();

            var client = _httpClientFactory.CreateClient("TGSClient");
            var toDelete = receipts.Where(x => x.IsSelected).ToArray();
            foreach (var item in toDelete)
            {
                var responce = await client.DeleteAsync($"/api/Receipts/DeleteReceipt/{item.Id.ToString()}");
                receipts.Remove(item);
            }
            Receipts = receipts.ToArray();
            return toDelete;
        }

        public void Select(Receipt receipt)
        {
            foreach (var item in Receipts)
            {
                item.IsSelected = false;
            }
            receipt.IsSelected = true;
        }

        public void ToggleSelect(Receipt receipt)
        {
            receipt.IsSelected = !receipt.IsSelected;
        }
        public void SelectRange(int start, int end)
        {
            foreach (var item in Receipts)
            {
                item.IsSelected = false;
            }
            for (int i = start; i < end; i++)
            {
                Receipts[i].IsSelected = true;
            }
        }
    }
}
