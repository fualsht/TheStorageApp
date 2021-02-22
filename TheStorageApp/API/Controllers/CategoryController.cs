﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
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
        public async Task<Category[]> GetCategories()
        {
            return await _context.Categories.
                ToArrayAsync();
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Route("GetCategory/{id}")]
        public async Task<Category> GetCategory(string id)
        {
            return await _context.Categories.
                FirstOrDefaultAsync(x => x.Id.ToString() == id);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Route("GetCategoryByName/{name}")]
        public async Task<Category> GetCategoryByName(string name)
        {
            return await _context.Categories.
                SingleOrDefaultAsync(x => x.Name == name);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
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
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Route("UpdateCategory")]
        public async Task<Category> UpdateCategory([FromBody]Category category)
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

        [HttpDelete]
        [Authorize(AuthenticationSchemes = "Bearer")]
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
