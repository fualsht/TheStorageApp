﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TheStorageApp.API.Data;
using TheStorageApp.API.Models;

namespace TheStorageApp.API.Controllers
{
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
        [Route("GetCategories")]
        public async Task<Category[]> GetCategories()
        {
            return await _context.Categories.
                Include(x => x.ModifiedBy).
                Include(x => x.CreatedBy).
                ToArrayAsync();
        }

        [HttpGet]
        [Route("GetCategory/{id}")]
        public async Task<Category> GetCategory(string id)
        {
            return await _context.Categories.
                Include(x => x.ModifiedBy).
                Include(x => x.CreatedBy).
                FirstOrDefaultAsync(x => x.Id.ToString() == id);
        }

        [HttpGet]
        [Route("GetCategoryByName/{name}")]
        public async Task<Category> GetCategoryByName(string name)
        {
            return await _context.Categories.
                Include(x => x.ModifiedBy).
                Include(x => x.CreatedBy).
                SingleOrDefaultAsync(x => x.Name == name);
        }

        [HttpPost]
        [Route("AddCategory")]
        public async Task<Category> AddCategory([FromBody] Category category)
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

        [HttpPut]
        [Route("UpdateCategory/{id}")]
        public async Task<Category> UpdateCategory(string id, [FromBody]Category category)
        {
            Category currentCategory = await _context.Categories.
                Include(x => x.ModifiedBy).
                Include(x => x.CreatedBy).
                FirstOrDefaultAsync(x => x.Id == id);

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

        [HttpDelete]
        [Route("DeleteCategory/{id}")]
        public async Task<Category> DeleteCategory(string id)
        {
            var categoryToDelete = await _context.Categories.
                FirstOrDefaultAsync(x => x.Id == id);

            _context.Categories.Remove(categoryToDelete);
            await _context.SaveChangesAsync();
            return categoryToDelete;
        }
    }
}
