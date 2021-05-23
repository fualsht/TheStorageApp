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
        [Route("GetModel")]
        public async Task<ActionResult<Model>> GetModel(string id)
        {
            var model = await _dataContext.Models.FirstOrDefaultAsync(x => x.Id == id);
            model.CreatedBy = await _dataContext.Users.FirstOrDefaultAsync(x => x.Id == model.CreatedById);
            return Ok(model);
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
                await _dataContext.Models.AddAsync(model);
                await _dataContext.SaveChangesAsync();
                return Ok(model);
            }
            catch (Exception ex)
            {
                return this.InternalServerError(ex, "Error");
            }
            
        }

        [HttpPost]
        //[Authorize(AuthenticationSchemes = "Bearer")]
        [Route("UpdateModel")]
        public ActionResult<Model>UpdateModel([FromBody]Model model)
        {
            var updatedmodel = _dataContext.Models.Update(model);
            return Ok(updatedmodel);
        }

        [HttpDelete]
        //[Authorize(AuthenticationSchemes = "Bearer")]
        [Route("DeleteModel")]
        public ActionResult<Model> DeleteModel([FromBody]Model model)
        {
            var updatedmodel = _dataContext.Models.Remove(model);
            return Ok(updatedmodel);
        }
    }
}
