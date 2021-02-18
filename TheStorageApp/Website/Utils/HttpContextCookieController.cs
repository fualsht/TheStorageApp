using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheStorageAppWebsite.Utils
{
    public class HttpContextCookieController : IHttpContextCookieController
    {
        private readonly IHttpContextAccessor _HttpContextAccessor;
        public HttpContextCookieController(IHttpContextAccessor httpContextAccessor)
        {
            _HttpContextAccessor = httpContextAccessor;
        }

        

        public void Delete(string key)
        {
            throw new NotImplementedException();
        }

        public string Get(string key)
        {
           return _HttpContextAccessor.HttpContext.Request.Cookies[key];
        }


        /// <summary>  
        /// set the cookie  
        /// </summary>  
        /// <param name="key">key (unique indentifier)</param>  
        /// <param name="value">value to store in cookie object</param>  
        /// <param name="expireTime">expiration time</param>  
        public void Set(string key, string value, int? expireTime)
        {
            CookieOptions option = new CookieOptions();

            if (expireTime.HasValue)
                option.Expires = DateTime.Now.AddMinutes(expireTime.Value);
            else
                option.Expires = DateTime.Now.AddMilliseconds(10);

            _HttpContextAccessor.HttpContext.Response.Cookies.Append(key, value, option);
        }


        /// <summary>  
        /// Delete the key  
        /// </summary>  
        /// <param name="key">Key</param>  
        public void Remove(string key)
        {
            _HttpContextAccessor.HttpContext.Response.Cookies.Delete(key);
        }


    }
}
