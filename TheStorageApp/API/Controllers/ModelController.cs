using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TheStorageApp.API.Data;
using TheStorageApp.API.Models;

namespace TheStorageApp.API.Controllers
{
    //[Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/Models")]
    [ApiController]    
    public class ModelController : APIControllerBase<Model>
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;

        public ModelController(ILogger<APIControllerBase<Model>> logger, DataContext dataContext, 
            IHttpContextAccessor httpContextAccessor, RoleManager<IdentityRole> roleManager,
            UserManager<AppUser> userManager, IConfiguration configuration) : 
            base(logger, dataContext, httpContextAccessor)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpGet]
        //[Authorize(AuthenticationSchemes = "Bearer")]
        [Route("GetModels")]
        public async Task<ActionResult<Model[]>> GetModels()
        {
            try
            {
                var models = await _dataContext.Models.ToArrayAsync();
                return Ok(models);
            }
            catch (Exception ex)
            {
                return this.InternalServerError(ex, "");
            }
        }

        [HttpGet]
        //[Authorize(AuthenticationSchemes = "Bearer")]
        [Route("GetModel/{id}")]
        public async Task<ActionResult<Model>> GetModel(string id)
        {
            try
            {
                var model = await _dataContext.Models.FirstOrDefaultAsync(x => x.Id == id);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return this.InternalServerError(ex, "");
            }
           
        }

        [HttpGet]
        [Route("GetModelByName/{name}")]
        public async Task<Model> GetModelByName(string name)
        {
            return await _dataContext.Models.SingleOrDefaultAsync(x => x.Name == name);
        }

        [HttpPost]
        [Route("AddModel")]
        //[Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<Model>> AddModel([FromBody]Model model)
        {
            try
            {
                Model newModel = new Model
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = model.Name,
                    PluralName = model.PluralName,
                    PrimaryColor = model.PrimaryColor,
                    SecondaryColor = model.SecondaryColor,
                    SmallImage = model.SmallImage,
                    LargeImage = model.LargeImage,
                    ModifiedById = model.ModifiedById,
                    CreatedById = model.CreatedById,
                    Description = model.Description,
                    ModifiedOn = model.ModifiedOn,
                    CreatedOn = model.CreatedOn
                };


                await _dataContext.Models.AddAsync(newModel);
                await _dataContext.SaveChangesAsync();

                List<Field> _fields = new List<Field>();
                foreach (var field in model.Fields)
                {
                    Field newField = new Field
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = field.Name,
                        Unique = field.Unique,
                        Requiered = field.Requiered,
                        ModifiedById = field.ModifiedById,
                        CreatedById = field.CreatedById,
                        CreatedOn = DateTime.Now,
                        ModifiedOn = DateTime.Now,
                        DataType = field.DataType,
                        DataTypeId = field.DataTypeId,
                        Description = field.Description,
                        MaxSize = field.MaxSize,
                        MinSize = field.MaxSize,
                        ModelId = newModel.Id
                    };
                    //_fields.Add(field);
                    await _dataContext.Fields.AddAsync(newField);
                }
                //await _dataContext.Fields.AddRangeAsync(_fields);
                await _dataContext.SaveChangesAsync();
                return Ok(model);
            }
            catch (Exception ex)
            {
                return this.InternalServerError(ex, "Error");
            }
            
        }

        [HttpPut]
        //[Authorize(AuthenticationSchemes = "Bearer")]
        [Route("UpdateModel")]
        public async Task<ActionResult<Model>> UpdateModel([FromBody]Model model)
        {
            try
            {
                var updatedmodel = _dataContext.Models.Update(model);
                var changecount = await _dataContext.SaveChangesAsync();
                if (changecount > 0)
                    return Ok(updatedmodel.Entity);
                else
                    return Ok(null);
                
            }
            catch (Exception ex)
            {
                return this.InternalServerError(ex, "Error");
            }
        }

        [HttpDelete]
        //[Authorize(AuthenticationSchemes = "Bearer")]
        [Route("DeleteModel")]
        public ActionResult<Model> DeleteModel([FromBody]Model model)
        {
            var updatedmodel = _dataContext.Models.Remove(model);
            return Ok(updatedmodel);
        }

        [HttpPost]
        //[Authorize(AuthenticationSchemes = "Bearer")]
        [Route("DeleteModels")]
        public async Task<ActionResult<bool>> DeleteModels([FromBody]Model[] models)
        {
            //todo: Delete Related Fields frist
            foreach (Model model in models)
            {
                var _model = await _dataContext.Models.FirstOrDefaultAsync(x => x.Id == model.Id);
                var fields = from flds in _dataContext.Fields where flds.ModelId == model.Id select flds;
                _dataContext.Fields.RemoveRange(fields);

                _dataContext.Models.Remove(_model);
            }

            var changecount = await _dataContext.SaveChangesAsync();

            if (changecount > 0)
                return Ok(true);
            else
                return Ok(false);
        }
    }
}
