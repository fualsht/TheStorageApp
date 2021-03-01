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
    public class CategoryController : APIControllerBase<Category>
    {
        public CategoryController(ILogger<CategoryController> logger, DataContext dataContext, IHttpContextAccessor httpContextAccessor) : base (logger,dataContext,httpContextAccessor)
        {
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Route("GetCategories")]
        public async Task<ActionResult<Category[]>> GetCategories()
        {
            try
            {
                var val = await _dataContext.Categories.ToArrayAsync();
                return Ok(val);
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
                return await _dataContext.Categories.FirstOrDefaultAsync(x => x.Id.ToString() == id);
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
                return await _dataContext.Categories.SingleOrDefaultAsync(x => x.Name == name);
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
                    CreatedById = HttpContext.Response.Headers["user"],
                    ModifiedById = HttpContext.Response.Headers["user"],
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now,
                };
                await _dataContext.Categories.AddAsync(newcategory);
                await _dataContext.SaveChangesAsync();
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
                        //CreatedById = this._contextCookieController.Get("user"),
                        //ModifiedById = this._contextCookieController.Get("user"),
                        CreatedOn = DateTime.Now,
                        ModifiedOn = DateTime.Now,
                    };
                    newCategories.Add(newcategory);
                }

                
                await _dataContext.Categories.AddRangeAsync(newCategories.ToArray());
                await _dataContext.SaveChangesAsync();

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
                Category currentCategory = await _dataContext.Categories.FirstOrDefaultAsync(x => x.Id == category.Id);

                if (currentCategory != null)
                {
                    if (!string.IsNullOrEmpty(category.Name))
                        currentCategory.Name = category.Name;

                    if (!string.IsNullOrEmpty(category.Color))
                        currentCategory.Color = category.Color;

                    //currentCategory.ModifiedById = this._contextCookieController.Get("user");
                    currentCategory.ModifiedOn = DateTime.Now;

                    _dataContext.Update(currentCategory);
                    await _dataContext.SaveChangesAsync();
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
                    Category currentCategory = await _dataContext.Categories.FirstOrDefaultAsync(x => x.Id == category.Id);

                    if (currentCategory != null)
                    {
                        if (!string.IsNullOrEmpty(category.Name))
                            currentCategory.Name = category.Name;

                        if (!string.IsNullOrEmpty(category.Color))
                            currentCategory.Color = category.Color;

                        //currentCategory.ModifiedById = this._contextCookieController.Get("user");
                        currentCategory.ModifiedOn = DateTime.Now;

                        updatedCategories.Add(currentCategory);
                    }
                }

                  _dataContext.UpdateRange(updatedCategories);
                  await _dataContext.SaveChangesAsync();

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
                var categoryToDelete = await _dataContext.Categories.FirstOrDefaultAsync(x => x.Id == id);

                _dataContext.Categories.Remove(categoryToDelete);
                await _dataContext.SaveChangesAsync();
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
                    categories.Add(await _dataContext.Categories.FirstOrDefaultAsync(x => x.Id == id));
                } 
                
                _dataContext.Categories.RemoveRange(categories);

                await _dataContext.SaveChangesAsync();

                return Ok(categories.ToArray());
            }
            catch (Exception exception)
            {
                return this.InternalServerError(exception, "Error deleting " + ids.Length.ToString() + " categories");
            }
        }
    }
}
