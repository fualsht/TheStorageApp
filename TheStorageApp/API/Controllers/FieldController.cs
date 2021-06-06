using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheStorageApp.API.Data;
using TheStorageApp.API.Models;

namespace TheStorageApp.API.Controllers
{
    [Route("api/Fields")]
    [ApiController]
    public class FieldController : APIControllerBase<Field>
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;

        public FieldController(ILogger<APIControllerBase<Field>> logger, DataContext dataContext, IHttpContextAccessor httpContextAccessor, 
            RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager, IConfiguration configuration) : 
            base(logger, dataContext, httpContextAccessor)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpGet]
        //[Authorize(AuthenticationSchemes = "Bearer")]
        [Route("GetFields")]
        public async Task<ActionResult<Field[]>> GetFields()
        {
            try
            {
                var fileds = await _dataContext.Fields.ToArrayAsync();
                return Ok(fileds);
            }
            catch (Exception ex)
            {
                return this.InternalServerError(ex, "");
            }
        }

        [HttpGet]
        //[Authorize(AuthenticationSchemes = "Bearer")]
        [Route("GetModel/{id}")]
        public async Task<ActionResult<Field>> GetField(string id)
        {
            try
            {
                var filed = await _dataContext.Fields.FirstOrDefaultAsync(x => x.Id == id);
                return Ok(filed);
            }
            catch (Exception ex)
            {
                return this.InternalServerError(ex, "");
            }

        }

        [HttpGet]
        [Route("GetFiledByName/{name}")]
        public async Task<Field> GetFiledByName(string name)
        {
            return await _dataContext.Fields.SingleOrDefaultAsync(x => x.Name == name);
        }

        [HttpPost]
        [Route("AddField")]
        //[Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<Model>> AddField([FromBody]Field field)
        {
            try
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
                    ModelId = field.ModelId
                };


                await _dataContext.Fields.AddAsync(newField);
                await _dataContext.SaveChangesAsync();
                return Ok(field);
            }
            catch (Exception ex)
            {
                return this.InternalServerError(ex, "Error");
            }

        }

        [HttpPost]
        [Route("AddFields")]
        //[Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<Model[]>> AddFields([FromBody]Field[] fields)
        {
            try
            {
                List<Field> _fields = new List<Field>();
                foreach (var field in fields)
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
                        ModelId = field.ModelId
                    };
                    _fields.Add(field);
                }

                await _dataContext.Fields.AddRangeAsync(_fields);
                await _dataContext.SaveChangesAsync();
                return Ok(_fields);
            }
            catch (Exception ex)
            {
                return this.InternalServerError(ex, "Error");
            }

        }

        [HttpPut]
        //[Authorize(AuthenticationSchemes = "Bearer")]
        [Route("UpdateField")]
        public async Task<ActionResult<Field>> UpdateField([FromBody] Field field)
        {
            try
            {
                var updatedfield = _dataContext.Fields.Update(field);
                var changecount = await _dataContext.SaveChangesAsync();
                if (changecount > 0)
                    return Ok(updatedfield.Entity);
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
        [Route("DeleteField")]
        public ActionResult<Field> DeleteField([FromBody] Field field)
        {
            var updatedfield = _dataContext.Fields.Remove(field);
            return Ok(updatedfield);
        }

        [HttpGet]
        //[Authorize(AuthenticationSchemes = "Bearer")]
        [Route("GetFieldsForModel/{parentid}")]
        public async Task<ActionResult<Field[]>> GetFieldsForModel(string parentid)
        {
            try
            {
                var fileds = (from f in _dataContext.Fields where f.ModelId == parentid select f);
                return Ok(fileds);
            }
            catch (Exception ex)
            {
                return this.InternalServerError(ex, "");
            }
        }
    }
}
