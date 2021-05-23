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
    public class ModelService : WebServiceBase<Model>
    {
        public List<Model> Models { get; set; }
        public ModelService(IHttpClientFactory httpClient, IHttpContextAccessor contextFactory, CookieController httpContextCookieController) : 
            base(httpClient, contextFactory, httpContextCookieController)
        {
        }

        public async Task GetModelsAsync()
        {
            var responce = await this.ApiGet("api/Models/GetModels");
            Models = new List<Model>();

            if (responce.IsSuccessStatusCode)
            {
                var models = await responce.Content.ReadFromJsonAsync<Model[]>();
                Models = models.ToList();
            }
        }
        public async Task<Model> AddModelAsync(Model model)
        {
            var responce = await ApiPost("api/Models/AddModel", model);
            var newModel = await responce.Content.ReadFromJsonAsync<Model>();
            return newModel;
        }

        public async Task<Model> UpdateModelAsync(Model model)
        {
            var client = _httpClientFactory.CreateClient("TGSClient");
            var responce = await client.PutAsJsonAsync<Model>($"/api/Users/UpdateUser/{model.Id}", model);
            var updatedModel = await responce.Content.ReadFromJsonAsync<Model>();

            return updatedModel;
        }

        public async Task<Model[]> DeleteModelAsync()
        {
            var models = Models.ToList<Model>();

            var client = _httpClientFactory.CreateClient("TGSClient");
            var toDelete = models.Where(x => x.IsSelected).ToArray();
            foreach (var item in toDelete)
            {
                var responce = await client.DeleteAsync($"/api/Models/DeleteModel/{item.Id.ToString()}");
                models.Remove(item);
            }
            Models = models;
            return toDelete;
        }

        public void Select(Model model)
        {
            foreach (var item in Models)
            {
                item.IsSelected = false;
            }
            model.IsSelected = true;
        }

        public void ToggleSelect(Model model)
        {
            model.IsSelected = !model.IsSelected;
        }
        public void SelectRange(int start, int end)
        {
            foreach (var item in Models)
            {
                item.IsSelected = false;
            }
            for (int i = start; i < end; i++)
            {
                Models[i].IsSelected = true;
            }
        }

        public Model NewModel(string userid)
        {
            Model model = new Model();
            model.Id = Guid.NewGuid().ToString();
            model.Name = "New";
            model.PluralName = "News";
            model.Description = "";
            model.PrimaryColor = "ffffff";
            model.SecondaryColor = "ffffff";
            model.SmallImage = new byte[0];
            model.LargeImage = new byte[0];
            model.CreatedOn = DateTime.Now;
            model.ModifiedOn = DateTime.Now;
            model.Id = Guid.NewGuid().ToString();
            model.Description = "";
            model.CreatedBy = null;
            model.ModifiedBy = null;
            model.CreatedById = userid;
            model.ModifiedById = userid;
            model.IsSelected = true;

            return model;
        }
    }
}
