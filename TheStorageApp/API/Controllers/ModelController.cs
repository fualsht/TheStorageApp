using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class ModelController : ControllerBase
    {
        private readonly ILogger<ModelController> _logger;
        private readonly DataContext _context;
        public ModelController(ILogger<ModelController> logger, DataContext dataContext)
        {
            _logger = logger;
            _context = dataContext;
        }

        [HttpGet]
        //[Authorize(AuthenticationSchemes = "Bearer")]
        [Route("GetReceipts")]
        public ActionResult<Model[]> GetModels()
        {
            var v = _context.Models.AsEnumerable().ToArray();
            return Ok(v);
        }

        [HttpGet]
        //[Authorize(AuthenticationSchemes = "Bearer")]
        [Route("GetReceipt")]
        public ActionResult<Model> GetModel(string id)
        {
            var v = _context.Models.SingleOrDefault(x => x.Id == id);
            return Ok(v);
        }

        [HttpPut]
        //[Authorize(AuthenticationSchemes = "Bearer")]
        [Route("GetReceipt")]
        public ActionResult<Model>UpdateModel([FromBody]Model model)
        {
            var updatedmodel = _context.Models.Update(model);
            return Ok(updatedmodel);
        }

        [HttpDelete]
        //[Authorize(AuthenticationSchemes = "Bearer")]
        [Route("GetReceipt")]
        public ActionResult<Model> DeleteModel([FromBody]Model model)
        {
            var updatedmodel = _context.Models.Remove(model);
            return Ok(updatedmodel);
        }
    }
}
