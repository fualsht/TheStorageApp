using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TheStorageApp.Website.Models;
using TheStorageApp.Website.Utils;

namespace TheStorageApp.Website.Services
{
    public class FieldService : WebServiceBase<Field>
    {
        List<Field> Fields { get; set; }
        public FieldService(IHttpClientFactory httpClient, IHttpContextAccessor contextFactory, CookieController httpContextCookieController) : 
            base(httpClient, contextFactory, httpContextCookieController)
        {
            Fields = new List<Field>();
        }

        public async Task GetFieldsAsync()
        {
            var responce = await this.ApiGet("api/Fields/GetFields");
            Fields = new List<Field>();

            if (responce.IsSuccessStatusCode)
            {
                var fields = await responce.Content.ReadFromJsonAsync<Field[]>();
                Fields = fields.ToList();
            }
        }

        public async Task<bool> AddFieldAsync(Field field)
        {
            var responce = await this.ApiPost("api/Fields/AddField", field);

            if (responce.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task<bool> AddFieldsAsync(Field[] fields)
        {
            var responce = await this.ApiPost("api/Fields/AddFields", fields);

            if (responce.IsSuccessStatusCode)
                return true;
            else
                return false;
        }
    }
}
