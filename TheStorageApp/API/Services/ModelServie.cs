

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using TheStorageApp.API.Data;
using TheStorageApp.API.Models;

namespace TheStorageApp.API.Services
{
    public class ModelServie : ServiceBase<Model>
    {
        public ModelServie(ILogger<ServiceBase<Model>> logger, DataContext dataContext, IHttpContextAccessor httpContextAccessor) : 
            base(logger, dataContext, httpContextAccessor)
        {
        }

        public async Task<ModelCommModel> GetModels()
        {
            ModelCommModel response = new ModelCommModel();

            try
            {
                var models = await _dataContext.Models.ToArrayAsync();
                string serilisedModelArray = JsonSerializer.Serialize<Model[]>(models);

                response.Content = serilisedModelArray;
                return response;
            }
            catch (Exception ex)
            {
                response.Error = ex.Message;
                return response;
            }
        }

        public class ModelCommModel
        {
            public object Content { get; set; }
            public object View { get; set; }
            public string Error { get; set; }
            public ModelCommModel()
            {

            }
        }
    }
}
