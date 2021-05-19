using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TheStorageApp.Website.Models;
using TheStorageApp.Website.Utils;

namespace TheStorageApp.Website.Services
{
    public class RolesService : WebServiceBase<AppRole>
    {
        public List<AppRole> Roles { get; set; }
        public RolesService(IHttpClientFactory httpClient, IHttpContextAccessor contextFactory, CookieController httpContextCookieController) :
             base(httpClient, contextFactory, httpContextCookieController)
        {

        }

        public async Task GetUserRolesAsync()
        {
            var responce = await this.ApiGet("api/Users/GetUserRoles");
            Roles = new List<AppRole>();

            if (responce.IsSuccessStatusCode)
            {
                var roles = await responce.Content.ReadFromJsonAsync<AppRole[]>();
                Roles = roles.ToList();
            }
        }

        public async Task DeleteUserRolesAsync()
        {
            //var responce = await this.ApiGet("api/Users/GetUserRoles");
            //Roles = new List<AppRole>();

            //if (responce.IsSuccessStatusCode)
            //{
            //    var roles = await responce.Content.ReadFromJsonAsync<AppRole[]>();
            //    Roles = roles.ToList();
            //}
        }

        public void Select(AppRole role)
        {
            foreach (var item in Roles)
            {
                item.IsSelected = false;
            }
            role.IsSelected = true;
        }

        public void ToggleSelect(AppRole role)
        {
            role.IsSelected = !role.IsSelected;
        }
        public void SelectRange(int start, int end)
        {
            foreach (var item in Roles)
            {
                item.IsSelected = false;
            }
            for (int i = start; i < end; i++)
            {
                Roles[i].IsSelected = true;
            }
        }
    }
}
