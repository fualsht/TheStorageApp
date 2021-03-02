using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TheStorageApp.Website.Model;

namespace TheStorageApp.Website.Services
{
    public class RolesService : IApiService<Role>
    {
        public Task<HttpResponseMessage> ApiDelete(string uri, string id)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> ApiDelete(string uri, string[] ids)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> ApiGet(string uri)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> ApiPost(string uri, Role t)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> ApiPost(string uri, Role[] t)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> ApiUpdate(string uri, Role t)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> ApiUpdate(string uri, Role[] t)
        {
            throw new NotImplementedException();
        }
    }
}
