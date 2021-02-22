using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TheStorageAppWebsite.Utils;

namespace TheStorageAppWebsite.Services
{
    public abstract class ApiServiceBase<T> : IApiService
    {
        protected readonly IHttpClientFactory _httpClientFactory;
        protected readonly IHttpContextAccessor _contextFactory;
        protected readonly HttpContextCookieController _httpContextCookieController;

        public ApiServiceBase(IHttpClientFactory httpClient, IHttpContextAccessor contextFactory, HttpContextCookieController httpContextCookieController)
        {
            _httpClientFactory = httpClient;
            _contextFactory = contextFactory;
            _httpContextCookieController = httpContextCookieController;
        }

        protected async Task<HttpResponseMessage> ApiGet(string uri)
        {
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get, uri);
            var client = _httpClientFactory.CreateClient("TGSClient");

            string token = _httpContextCookieController.Get("token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.SendAsync(httpRequest);
            return response;
        }

        public async Task<HttpResponseMessage> ApiPost(string uri, T t)
        {
            var client = _httpClientFactory.CreateClient("TGSClient");

            string token = _httpContextCookieController.Get("token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var responce = await client.PostAsJsonAsync<T>(uri, t);

            return responce;
        }

        public async Task<HttpResponseMessage> ApiUpdate(string uri, T t)
        {
            var client = _httpClientFactory.CreateClient("TGSClient");

            string token = _httpContextCookieController.Get("token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var responce = await client.PutAsJsonAsync<T>(uri, t);

            return responce;
        }

        public async Task<HttpResponseMessage> ApiDelete(string uri)
        {
            var client = _httpClientFactory.CreateClient("TGSClient");

            string token = _httpContextCookieController.Get("token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var responce = await client.DeleteAsync(uri);

            return responce;
        }
    }
}
