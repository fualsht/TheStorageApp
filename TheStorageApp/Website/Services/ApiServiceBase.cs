﻿using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TheStorageApp.Website.Services;
using TheStorageApp.Website.Utils;

namespace TheStorageApp.Website.Services
{
    public abstract class ApiServiceBase<T> : IApiService<T>
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

        /// <summary>
        /// A helper function that calls a Web API endPoint with the HttpMethod.Get method, wraped with the endPoint Athentication Token.
        /// </summary>
        /// <param name="uri">The Apie Endpoint to call</param>
        /// <returns>The responce object from the controller, No error checking is performed on this call.</returns>
        public virtual async Task<HttpResponseMessage> ApiGet(string uri)
        {
            var client = _httpClientFactory.CreateClient("TGSClient");

            string token = _httpContextCookieController.Get("token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync(uri);
            return response;
        }

        /// <summary>
        /// A helper function that calls a Web API endPoint with the HttpMethod.Get method, wraped with the endPoint Athentication Token.
        /// </summary>
        /// <param name="uri">The Apie Endpoint to call</param>
        /// <returns>The responce object from the controller, No error checking is performed on this call.</returns>
        public virtual async Task<HttpResponseMessage> ApiGet(string uri, string[] ids)
        {
            var client = _httpClientFactory.CreateClient("TGSClient");

            string token = _httpContextCookieController.Get("token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.PostAsJsonAsync<string[]>(uri, ids);

            return response;
        }

        /// <summary>
        /// A helper function that calls a Web API endPoint with the HttpMethod.Post method, wraped with the endPoint Athentication Token.
        /// </summary>
        /// <param name="uri">The Uri endpoint to call</param>
        /// <param name="t">The object to post</param>
        /// <returns>The responce object from the controller, No error checking is performed on this call.</returns>
        public virtual async Task<HttpResponseMessage> ApiPost(string uri, T t)
        {
            var client = _httpClientFactory.CreateClient("TGSClient");

            string token = _httpContextCookieController.Get("token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var responce = await client.PostAsJsonAsync<T>(uri, t);

            return responce;
        }

        /// <summary>
        /// A helper function that calls a Web API endPoint with the HttpMethod.Post method, wraped with the endPoint Athentication Token.
        /// </summary>
        /// <param name="uri">The Uri endpoint to call</param>
        /// <param name="t">The objects to post</param>
        /// <returns>The responce object from the controller, No error checking is performed on this call.</returns>
        public virtual async Task<HttpResponseMessage> ApiPost(string uri, T[] t)
        {
            var client = _httpClientFactory.CreateClient("TGSClient");

            string token = _httpContextCookieController.Get("token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var responce = await client.PostAsJsonAsync<T[]>(uri, t);

            return responce;
        }

        /// <summary>
        /// A helper function that calls a Web API endPoint with the HttpMethod.Put method, wraped with the endPoint Athentication Token.
        /// </summary>
        /// <param name="uri">The Uri endpoint to call</param>
        /// <param name="t">The object to update</param>
        /// <returns>The responce object from the controller, No error checking is performed on this call.</returns>
        public virtual async Task<HttpResponseMessage> ApiUpdate(string uri, T t)
        {
            var client = _httpClientFactory.CreateClient("TGSClient");

            string token = _httpContextCookieController.Get("token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var responce = await client.PutAsJsonAsync<T>(uri, t);

            return responce;
        }

        /// <summary>
        /// A helper function that calls a Web API endPoint with the HttpMethod.Put method, wraped with the endPoint Athentication Token.
        /// </summary>
        /// <param name="uri">The Uri endpoint to call</param>
        /// <param name="t">The objects to update</param>
        /// <returns>The responce objects from the controller, No error checking is performed on this call.</returns>
        public virtual async Task<HttpResponseMessage> ApiUpdate(string uri, T[] t)
        {
            var client = _httpClientFactory.CreateClient("TGSClient");

            string token = _httpContextCookieController.Get("token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var responce = await client.PutAsJsonAsync<T[]>(uri, t);

            return responce;
        }

        /// <summary>
        /// A helper function that calls a Web API endPoint with the HttpMethod.Delete method, wraped with the endPoint Athentication Token.
        /// </summary>
        /// <param name="uri">The Uri endpoint to call</param>
        /// <param name="t">The objects to Delete</param>
        /// <returns>The responce object from the controller, No error checking is performed on this call.</returns>
        public virtual async Task<HttpResponseMessage> ApiDelete(string uri, string id)
        {
            var client = _httpClientFactory.CreateClient("TGSClient");

            string token = _httpContextCookieController.Get("token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var responce = await client.DeleteAsync(uri + "/" + id);

            return responce;
        }

        /// <summary>
        /// A helper function that calls a Web API endPoint with the HttpMethod.Delete method, wraped with the endPoint Athentication Token.
        /// </summary>
        /// <param name="uri">The Uri endpoint to call</param>
        /// <param name="t">The objects to Delete</param>
        /// <returns>The responce object from the controller, No error checking is performed on this call.</returns>
        public virtual async Task<HttpResponseMessage> ApiDelete(string uri, string[] ids)
        {
            var client = _httpClientFactory.CreateClient("TGSClient");

            string token = _httpContextCookieController.Get("token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var responce = await client.PostAsJsonAsync<string[]>(uri, ids);

            return responce;
        }

    }
}
