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
        public FieldService(IHttpClientFactory httpClient, IHttpContextAccessor contextFactory, CookieController httpContextCookieController) : 
            base(httpClient, contextFactory, httpContextCookieController)
        {
        }

        public async Task<Field[]> GetFieldsAsync()
        {
            var responce = await this.ApiGet("api/Fields/GetFields");

            if (responce.IsSuccessStatusCode)
            {
                var fields = await responce.Content.ReadFromJsonAsync<Field[]>();
                return fields.ToArray();
            }
            else
            {
                return null;
            }
        }

        public async Task<Field> GetFieldAsync(string id)
        {
            var responce = await this.ApiGet("api/Fields/GetField/" + id);

            if (responce.IsSuccessStatusCode)
            {
                var field = await responce.Content.ReadFromJsonAsync<Field>();
                return field;
            }
            else
            {
                return null;
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
