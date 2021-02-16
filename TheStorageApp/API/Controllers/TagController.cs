using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheStorageApp.API.Data;
using TheStorageApp.API.Models;

namespace TheGrocerWebApi.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ILogger<TagController> _logger;
        private readonly DataContext _context;

        public TagController(ILogger<TagController> logger, DataContext dataContext)
        {
            _logger = logger;
            _context = dataContext;
        }

        [HttpGet]
        [Route("GetTags")]
        public async Task<Tag[]> GetTags()
        {
            return await _context.Tags.ToArrayAsync();
        }

        [HttpGet]
        [Route("GetTags/{id}")]
        public async Task<Tag> GetTag(string id)
        {
            return await _context.Tags.FirstOrDefaultAsync(x => x.Id.ToString() == id);
        }

        [HttpGet]
        [Route("GetTagByName/{name}")]
        public async Task<Tag> GetTagByName(string name)
        {
            return await _context.Tags.SingleOrDefaultAsync(x => x.Name == name);
        }

        [HttpPost]
        [Route("AddTag")]
        public async Task<Tag> AddTag([FromBody]Tag tag)
        {
            Tag newTag = new Tag()
            {
                Id = Guid.NewGuid().ToString(),
                Name = tag.Name,
                Color = tag.Color,
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now
            };

            await _context.Tags.AddAsync(newTag);
            await _context.SaveChangesAsync();
            return newTag;
        }

        [HttpPut]
        [Route("UpdateTag/{id}")]
        public async Task<Tag> UpdateTag(string id, [FromBody]Tag tag)
        {
            Tag currentTag = await _context.Tags.FirstOrDefaultAsync(x => x.Id == id);

            if (currentTag != null)
            {
                if (!string.IsNullOrEmpty(tag.Name))
                    currentTag.Name = tag.Name;

                if (!string.IsNullOrEmpty(tag.Color))
                    currentTag.Color = tag.Color;

                currentTag.ModifiedOn = DateTime.Now;

                _context.Update(currentTag);
                await _context.SaveChangesAsync();
            }

            return currentTag;
        }

        [HttpDelete]
        [Route("DeleteTag/{id}")]
        public async Task<Tag> DeleteTag(string id)
        {
            var tagToDelete = await _context.Tags.FirstOrDefaultAsync(x => x.Id == id);
            _context.Tags.Remove(tagToDelete);
            await _context.SaveChangesAsync();
            return tagToDelete;
        }
    }
}
