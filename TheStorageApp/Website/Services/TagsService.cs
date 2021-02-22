using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TheStorageApp.Shared.Models;
using TheStorageApp.Website.Utils;

namespace TheStorageApp.Website.Services
{
    public class TagsService : ApiServiceBase<Tag>
    {
        public Tag[] Tags { get; set; }

        public TagsService(IHttpClientFactory httpClient, IHttpContextAccessor contextFactory, HttpContextCookieController httpContextCookieController) : 
            base(httpClient, contextFactory, httpContextCookieController)
        {
        }

        public async Task GetTagsAsync()
        {
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get, "/api/Tags/GetTags");

            var client = _httpClientFactory.CreateClient("TGSClient");
            var response = await client.SendAsync(httpRequest);

            if (response.IsSuccessStatusCode)
            {
                Tags = await response.Content.ReadFromJsonAsync<Tag[]>();
            }
        }

        public async Task<Tag> AddTagAsync(Tag tag)
        {
            var client = _httpClientFactory.CreateClient("TGSClient");
            var responce = await client.PostAsJsonAsync<Tag>("/api/Tags/AddTag", tag);
            var newReceipt = await responce.Content.ReadFromJsonAsync<Tag>();
            
            return newReceipt;
        }

        public async Task<Tag> UpdateTagAsync(Tag tag)
        {
            var client = _httpClientFactory.CreateClient("TGSClient");
            var responce = await client.PutAsJsonAsync<Tag>($"/api/Tags/UpdateTag/{tag.Id.ToString()}", tag);
            var updatedRecipet = await responce.Content.ReadFromJsonAsync<Tag>();

            return updatedRecipet;
        }

        public async Task<Tag[]> DeleteTagsAsync()
        {
            var client = _httpClientFactory.CreateClient("TGSClient");
            var toDelete = Tags.Where(x => x.IsSelected).ToArray();
            foreach (var item in toDelete)
            {
                var responce = await client.DeleteAsync($"/api/Tags/DeleteTag/{item.Id.ToString()}");
            }
            return toDelete;
        }

        public void Select(Tag tag)
        {
            foreach (var item in Tags)
            {
                item.IsSelected = false;
            }
            tag.IsSelected = true;
        }

        public void ToggleSelect(Tag tag)
        {
            tag.IsSelected = !tag.IsSelected;
        }
        public void SelectRange(int start, int end)
        {
            foreach (var item in Tags)
            {
                item.IsSelected = false;
            }
            for (int i = start; i < end; i++)
            {
                Tags[i].IsSelected = true;
            }
        }
    }
}
