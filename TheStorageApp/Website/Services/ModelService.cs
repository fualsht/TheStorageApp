using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TheStorageApp.Website.Models;
using TheStorageApp.Website.Utils;

namespace TheStorageApp.Website.Services
{
    public class ModelService : WebServiceBase<Model>
    {
        //public List<Model> Models { get; set; }
        public ModelService(IHttpClientFactory httpClient, IHttpContextAccessor contextFactory, CookieController httpContextCookieController) : 
            base(httpClient, contextFactory, httpContextCookieController)
        {
        }

        public async Task<Model[]> GetModelsAsync()
        {
            var responce = await this.ApiGet("api/Models/GetModels");
            var Models = new List<Model>();

            if (responce.IsSuccessStatusCode)
            {
                var models = await responce.Content.ReadFromJsonAsync<Model[]>();
                Models = models.ToList();
            }

            return Models.ToArray();
        }

        public async Task<Model> GetModelAsync(string id)
        {
            try
            {
                var modelresponce = await this.ApiGet("api/Models/GetModel/" + id);
                var fieldsresponce = await this.ApiGet("api/Fields/GetFieldsForModel/" + id);

                if (modelresponce.IsSuccessStatusCode)
                {
                    var model = await modelresponce.Content.ReadFromJsonAsync<Model>();
                    var fields = await fieldsresponce.Content.ReadFromJsonAsync<Field[]>();
                    model.Fields = fields;
                    return model;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {

                throw;
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
            var responce = await client.PutAsJsonAsync<Model>($"/api/Models/UpdateModel", model);
            var updatedModel = await responce.Content.ReadFromJsonAsync<Model>();

            return updatedModel;
        }

        public async Task<bool> DeleteModelAsync(string id)
        {
            var responce = await this.ApiDelete($"/api/Models/DeleteModel", id);

            if (responce.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task<bool> DeleteModelsAsync(Model[] models)
        {
            var responce = await this.ApiDelete($"/api/Models/DeleteModels", models);

            if (responce.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public void Select(Model[] array, Model model)
        {
            foreach (var item in array)
            {
                item.IsSelected = false;
            }
            model.IsSelected = true;
        }

        public void ToggleSelect(Model model)
        {
            model.IsSelected = !model.IsSelected;
        }
        public void SelectRange(Model[] array, int start, int end)
        {
            foreach (var item in array)
            {
                item.IsSelected = false;
            }
            for (int i = start; i < end; i++)
            {
                array[i].IsSelected = true;
            }
        }

        public Model NewModel(string userid)
        {
            Model model = new Model();
            model.Id = Guid.NewGuid().ToString();
            model.Name = "NewField";
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

            List<Field> fields = new List<Field>();
            Field idField = Field.NewField(model.Id);
            idField.Name = "Id";
            idField.DataType = DataType.PrimaryKey;
            idField.Description = "Default Id Field for the newly created Model.";
            idField.CreatedOn = DateTime.Now;
            idField.ModifiedOn = DateTime.Now;
            idField.CreatedById = userid;
            idField.ModifiedById = userid;
            idField.Requiered = true;
            idField.Unique = true;
            idField.MinSize = 0;
            idField.MaxSize = 36;
            fields.Add(idField);

            Field nameField = Field.NewField(model.Id);
            nameField.Name = "NewField";
            nameField.DataType = DataType.Text;
            nameField.Description = "The name of a model.";
            nameField.CreatedOn = DateTime.Now;
            nameField.ModifiedOn = DateTime.Now;
            nameField.CreatedById = userid;
            nameField.ModifiedById = userid;
            nameField.Requiered = true;
            nameField.Unique = true;
            nameField.MinSize = 0;
            nameField.MaxSize = 512;
            fields.Add(nameField);

            Field descriptionField = Field.NewField(model.Id);
            descriptionField.Name = "Description";
            descriptionField.DataType = DataType.Text;
            descriptionField.Description = "A description of a model.";
            descriptionField.CreatedOn = DateTime.Now;
            descriptionField.ModifiedOn = DateTime.Now;
            descriptionField.CreatedById = userid;
            descriptionField.ModifiedById = userid;
            descriptionField.Requiered = true;
            descriptionField.Unique = true;
            descriptionField.MinSize = 0;
            descriptionField.MaxSize = 4096;
            fields.Add(descriptionField);

            Field dateCreateField = Field.NewField(model.Id);
            dateCreateField.Name = "CreatedOn";
            dateCreateField.DataType = DataType.DateTime;
            dateCreateField.Description = "The date a model was created.";
            dateCreateField.CreatedOn = DateTime.Now;
            dateCreateField.ModifiedOn = DateTime.Now;
            dateCreateField.CreatedById = userid;
            dateCreateField.ModifiedById = userid;
            dateCreateField.Requiered = true;
            dateCreateField.Unique = true;
            dateCreateField.MinSize = 0;
            dateCreateField.MaxSize = 0;
            fields.Add(dateCreateField);

            Field dateModifiedField = Field.NewField(model.Id);
            dateModifiedField.Name = "ModifiedOn";
            dateModifiedField.DataType = DataType.DateTime;
            dateModifiedField.Description = "The date a model was Modified.";
            dateModifiedField.CreatedOn = DateTime.Now;
            dateModifiedField.ModifiedOn = DateTime.Now;
            dateModifiedField.CreatedById = userid;
            dateModifiedField.ModifiedById = userid;
            dateModifiedField.Requiered = true;
            dateModifiedField.Unique = true;
            dateModifiedField.MinSize = 0;
            dateModifiedField.MaxSize = 0;
            fields.Add(dateModifiedField);

            Field createdByField = Field.NewField(model.Id);
            createdByField.Name = "CreatedBy";
            createdByField.DataType = DataType.LookUp;
            createdByField.Description = "The User who created the model.";
            createdByField.CreatedOn = DateTime.Now;
            createdByField.ModifiedOn = DateTime.Now;
            createdByField.CreatedById = userid;
            createdByField.ModifiedById = userid;
            createdByField.Requiered = true;
            createdByField.Unique = true;
            createdByField.MinSize = 0;
            createdByField.MaxSize = 0;
            fields.Add(createdByField);

            Field createdByIdField = Field.NewField(model.Id);
            createdByIdField.Name = "CreatedById";
            createdByIdField.DataType = DataType.ForeignKey;
            createdByIdField.Description = "The User Id who created the model.";
            createdByIdField.CreatedOn = DateTime.Now;
            createdByIdField.ModifiedOn = DateTime.Now;
            createdByIdField.CreatedById = userid;
            createdByIdField.ModifiedById = userid;
            createdByIdField.Requiered = true;
            createdByIdField.Unique = true;
            createdByIdField.MinSize = 0;
            createdByIdField.MaxSize = 36;
            fields.Add(createdByIdField);

            Field modifiedByField = Field.NewField(model.Id);
            modifiedByField.Name = "ModifiedBy";
            modifiedByField.DataType = DataType.LookUp;
            modifiedByField.Description = "The User who Modified the model.";
            modifiedByField.CreatedOn = DateTime.Now;
            modifiedByField.ModifiedOn = DateTime.Now;
            modifiedByField.CreatedById = userid;
            modifiedByField.ModifiedById = userid;
            modifiedByField.Requiered = true;
            modifiedByField.Unique = true;
            modifiedByField.MinSize = 0;
            modifiedByField.MaxSize = 0;
            fields.Add(modifiedByField);

            Field modifiedByIdField = Field.NewField(model.Id);
            modifiedByIdField.Name = "ModifiedById";
            modifiedByIdField.DataType = DataType.ForeignKey;
            modifiedByIdField.Description = "The User Id who Modified the model.";
            modifiedByIdField.CreatedOn = DateTime.Now;
            modifiedByIdField.ModifiedOn = DateTime.Now;
            modifiedByIdField.CreatedById = userid;
            modifiedByIdField.ModifiedById = userid;
            modifiedByIdField.Requiered = true;
            modifiedByIdField.Unique = true;
            modifiedByIdField.MinSize = 0;
            modifiedByIdField.MaxSize = 36;
            fields.Add(modifiedByIdField);

            model.Fields = fields;

            return model;
        }
    }
}
