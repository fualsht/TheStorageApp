using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheStorageAppWebsite.Utils
{
    public interface IHttpContextCookieController
    {
        string Get(string key);
        void Set(string key, string value, int? expireTime);
        void Delete(string key);
    }
}
