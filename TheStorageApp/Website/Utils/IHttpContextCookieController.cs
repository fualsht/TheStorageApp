using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheStorageApp.Website.Utils
{
    public interface IHttpContextCookieController
    {
        string Get(string key);
        void Set(string key, string value, DateTime expire);
        void Delete(string key);
    }
}
