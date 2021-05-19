using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TheStorageApp.Website.Utils;

namespace TheStorageApp.Website.Services
{
    public class ServerControllerService : WebServiceBase<string>
    {
        public ServerControllerService(IHttpClientFactory httpClient, IHttpContextAccessor contextFactory, CookieController httpContextCookieController) : 
            base(httpClient, contextFactory, httpContextCookieController)
        {
        }

        public async Task<StreamReader> RunCommand(string command)
        {
            var response = await this.ApiPost($"/api/ServerCommand/ExecuteCMDCommand1", command);
            var stream = await response.Content.ReadAsStreamAsync();
            return new StreamReader(stream);
        }
    }
}
