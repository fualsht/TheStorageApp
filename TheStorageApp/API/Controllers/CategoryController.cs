using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheStorageApp.API.Data;
using TheStorageApp.API.Models;

namespace TheStorageApp.API.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/Categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly DataContext _context;

        public CategoryController(ILogger<CategoryController> logger, DataContext dataContext)
        {
            _logger = logger;
            _context = dataContext;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Route("GetCategories")]
        public async Task<ActionResult<Category[]>> GetCategories()
        {
            try
            {
                return await _context.Categories.ToArrayAsync();
            }
            catch (Exception exception)
            {
                return this.InternalServerError(exception, "Error getting categories");
            }
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Route("GetCategory/{id}")]
        public async Task<ActionResult<Category>> GetCategory(string id)
        {
            try
            {
                return await _context.Categories.FirstOrDefaultAsync(x => x.Id.ToString() == id);
            }
            catch (Exception exception)
            {
                return this.InternalServerError(exception, "Error getting category with ID: " + id);
            }
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Route("GetCategoryByName/{name}")]
        public async Task<ActionResult<Category>> GetCategoryByName(string name)
        {
            try
            {
                return await _context.Categories.SingleOrDefaultAsync(x => x.Name == name);
            }
            catch (Exception exception)
            {
                return this.InternalServerError(exception, "Error getting category with Name: " + name);
            }
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Route("AddCategory")]
        public async Task<ActionResult<Category>> AddCategory([FromBody] Category category)
        {
            try
            {
                Category newcategory = new Category()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = category.Name,
                    Color = category.Color,
                    CreatedBy = await _context.Users.FirstOrDefaultAsync(),
                    ModifiedBy = await _context.Users.FirstOrDefaultAsync(),
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now,
                };
                await _context.Categories.AddAsync(newcategory);
                await _context.SaveChangesAsync();
                return newcategory;
            }
            catch (Exception exception)
            {
                return this.InternalServerError(exception, "Error creating category with Id: " + category.Id);
            }
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Route("AddCategories")]
        public async Task<ActionResult<Category[]>> AddCategories([FromBody]Category[] categories)
        {
            try
            {
                List<Category> newCategories = new List<Category>();

                foreach (var category in categories)
                {
                    Category newcategory = new Category()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = category.Name,
                        Color = category.Color,
                        CreatedBy = await _context.Users.FirstOrDefaultAsync(),
                        ModifiedBy = await _context.Users.FirstOrDefaultAsync(),
                        CreatedOn = DateTime.Now,
                        ModifiedOn = DateTime.Now,
                    };
                    newCategories.Add(newcategory);
                }

                
                await _context.Categories.AddRangeAsync(newCategories.ToArray());
                await _context.SaveChangesAsync();

                return newCategories.ToArray();
            }
            catch (Exception exception)
            {
                return this.InternalServerError(exception, "Error creating " + categories.Length.ToString() + " categories");
            }
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Route("UpdateCategory")]
        public async Task<ActionResult<Category>> UpdateCategory([FromBody]Category category)
        {
            try
            {
                Category currentCategory = await _context.Categories.
                            FirstOrDefaultAsync(x => x.Id == category.Id);

                if (currentCategory != null)
                {
                    if (!string.IsNullOrEmpty(category.Name))
                        currentCategory.Name = category.Name;

                    if (!string.IsNullOrEmpty(category.Color))
                        currentCategory.Color = category.Color;

                    currentCategory.ModifiedBy = await _context.Users.FirstOrDefaultAsync();
                    currentCategory.ModifiedOn = DateTime.Now;

                    _context.Update(currentCategory);
                    await _context.SaveChangesAsync();
                }

                return currentCategory;
            }
            catch (Exception exception)
            {
                return this.InternalServerError(exception, "Error updating category with Id: " + category.Id);
            }
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Route("UpdateCategories")]
        public async Task<ActionResult<Category[]>> UpdateCategories([FromBody]Category[] categories)
        {
            try
            {
                List<Category> updatedCategories = new List<Category>();

                foreach (var category in categories)
                {
                    Category currentCategory = await _context.Categories.FirstOrDefaultAsync(x => x.Id == category.Id);

                    if (currentCategory != null)
                    {
                        if (!string.IsNullOrEmpty(category.Name))
                            currentCategory.Name = category.Name;

                        if (!string.IsNullOrEmpty(category.Color))
                            currentCategory.Color = category.Color;

                        currentCategory.ModifiedBy = await _context.Users.FirstOrDefaultAsync();
                        currentCategory.ModifiedOn = DateTime.Now;

                        updatedCategories.Add(currentCategory);
                    }
                }

                  _context.UpdateRange(updatedCategories);
                  await _context.SaveChangesAsync();

                return updatedCategories.ToArray();
            }
            catch (Exception exception)
            {
                return this.InternalServerError(exception, "Error updating " + categories.Length.ToString() + " categories");
            }
        }

        [HttpDelete]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Route("DeleteCategory/{id}")]
        public async Task<ActionResult<Category>> DeleteCategory(string id)
        {
            try
            {
                var categoryToDelete = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);

                _context.Categories.Remove(categoryToDelete);
                await _context.SaveChangesAsync();
                return categoryToDelete;
            }
            catch (Exception exception)
            {
                return this.InternalServerError(exception, "Error deleting category with Id: " + id);
            }
        }

        [HttpDelete]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Route("DeleteCategories")]
        public async Task<ActionResult<Category[]>> DeleteCategories([FromBody]string[] ids)
        {
            try
            {
                List<Category> categories = new List<Category>();

                foreach (var id in ids)
                {
                    categories.Add(await _context.Categories.FirstOrDefaultAsync(x => x.Id == id));
                } 
                
                _context.Categories.RemoveRange(categories);

                await _context.SaveChangesAsync();

                return Ok(categories.ToArray());
            }
            catch (Exception exception)
            {
                return this.InternalServerError(exception, "Error deleting " + ids.Length.ToString() + " categories");
            }
        }
    }
}
