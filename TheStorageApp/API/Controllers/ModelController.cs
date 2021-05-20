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
    [Route("api/[controller]")]
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
        public ActionResult<Model[]> GetModels()
        {
            var v = _dataContext.Models.AsEnumerable().ToArray();
            return Ok(v);
        }

        [HttpGet]
        //[Authorize(AuthenticationSchemes = "Bearer")]
        [Route("GetModel")]
        public ActionResult<Model> GetModel(string id)
        {
            var v = _dataContext.Models.SingleOrDefault(x => x.Id == id);
            return Ok(v);
        }

        [HttpGet]
        [Route("GetModelByName/{name}")]
        public async Task<Model> GetModelByName(string name)
        {
            return await _dataContext.Models.SingleOrDefaultAsync(x => x.Name == name);
        }

        [HttpPost]
        [Route("AddModel")]
        public async Task<Model> AddModel([FromBody] Model model)
        {
            Model newModel = new Model()
            {
                Id = Guid.NewGuid().ToString(),
                CreatedById = "",
                CreatedBy = null,
                CreatedOn = DateTime.Now,
                ModifiedById = Guid.NewGuid().ToString(),
                ModifiedBy = null,
                ModifiedOn = DateTime.Now,
                Description = "",
                LargeImage = new byte[0],
                SmallImage = new byte[0],
                Name = "",
                PluralName = "",
                SecondaryColor = 0,
                PrimaryColor = 0
            };

            await _dataContext.Models.AddAsync(newModel);
            await _dataContext.SaveChangesAsync();
            return newModel;
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
