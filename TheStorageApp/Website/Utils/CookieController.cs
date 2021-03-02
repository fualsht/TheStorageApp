using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace TheStorageApp.Website.Utils
{
    public class CookieController : ICookieController
    {
        private readonly IHttpContextAccessor _HttpContextAccessor;
        public CookieController(IHttpContextAccessor httpContextAccessor)
        {
            _HttpContextAccessor = httpContextAccessor;
        }

        

        public void Delete(string key)
        {
            CookieOptions op = new CookieOptions { Expires = DateTime.Now.AddMinutes(-10) };
            _HttpContextAccessor.HttpContext.Response.Cookies.Append("token", "", op);
        }

        public string Get(string key)
        {
            string value = _HttpContextAccessor.HttpContext.Request.Cookies[key];
            return _HttpContextAccessor.HttpContext.Request.Cookies[key];
        }


        /// <summary>  
        /// set the cookie  
        /// </summary>  
        /// <param name="key">key (unique indentifier)</param>  
        /// <param name="value">value to store in cookie object</param>  
        /// <param name="expireTime">expiration time</param>  
        public void Set(string key, string value, DateTime expire)
        {
            CookieOptions option = new CookieOptions();

            option.Expires = expire;

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
