using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TheStorageApp.API.Data;
using TheStorageApp.Shared.Models;

namespace TheGrocerWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly ILogger<ShopController> _logger;
        private readonly DataContext _context;

        public ShopController(ILogger<ShopController> logger, DataContext dataContext)
        {
            _logger = logger;
            _context = dataContext;
        }

        [HttpGet]
        [Route("GetShops")]
        public async Task<Shop[]> GetShops()
        {
            return await _context.Shops.
                Include(x => x.CreatedBy).
                Include(x => x.ModifiedBy).
                ToArrayAsync();
        }

        [HttpGet]
        [Route("GetShop/{id}")]
        public async Task<Shop> GetShop(string id)
        {
            return await _context.Shops.
                Include(x => x.CreatedBy).
                Include(x => x.ModifiedBy).
                FirstOrDefaultAsync(e => e.Id == new Guid(id));
        }

        [HttpGet]
        [Route("GetShop/{name}")]
        public async Task<Shop> GetShopByName(string name)
        {
            return await _context.Shops.
                Include(x => x.CreatedBy).
                Include(x => x.ModifiedBy).
                FirstOrDefaultAsync(e => e.Name == name);
        }

        [HttpPost]
        [Route("AddShop")]
        public async Task<Shop> AddShop([FromBody]Shop shop)
        {
            Shop newshop = new Shop()
            {
                Id = Guid.NewGuid(),
                Name = shop.Name,
                GPSLocation = shop.GPSLocation,
                Website = shop.Website,
                CreatedBy = await _context.Users.FirstOrDefaultAsync(),
                ModifiedBy = await _context.Users.FirstOrDefaultAsync(),
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now
            };

            await _context.Shops.AddAsync(newshop);
            await _context.SaveChangesAsync();
            return newshop;
        }

        [HttpPut]
        [Route("UpdateShop/{id}")]
        public async Task<Shop> UpdateShop(string id, [FromBody]Shop shop)
        {
            Shop currentShop = await _context.Shops.
                Include(x => x.CreatedBy).
                Include(x => x.ModifiedBy).
                FirstOrDefaultAsync(x => x.Id == new Guid(id));

            if (currentShop != null)
            {
                if (!string.IsNullOrEmpty(shop.Name))
                    currentShop.Name = shop.Name;

                if (!string.IsNullOrEmpty(shop.GPSLocation))
                    currentShop.GPSLocation = shop.GPSLocation;

                if (!string.IsNullOrEmpty(shop.Website))
                    currentShop.Website = shop.Website;

                currentShop.ModifiedBy = await _context.Users.FirstOrDefaultAsync();
                currentShop.ModifiedOn = DateTime.Now;

                _context.Update(currentShop);
                await _context.SaveChangesAsync();
            }

            return currentShop;
        }

        [HttpDelete]
        [Route("DeleteShop/{id}")]
        public async Task<Shop> DeleteShop(string id)
        {
            var shopToDelete = await _context.Shops.FirstOrDefaultAsync(x => x.Id == new Guid(id));
            _context.Shops.Remove(shopToDelete);
            await _context.SaveChangesAsync();
            return shopToDelete;
        }

    }
}
