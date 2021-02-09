using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TheStorageApp.API.Data;
using TheStorageApp.Shared.Models;

namespace TheGrocerWebApi.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class ReceiptImageController : ControllerBase
    {
        private readonly ILogger<ReceiptImageController> _logger;
        private readonly DataContext _context;

        public ReceiptImageController(ILogger<ReceiptImageController> logger, DataContext dataContext)
        {
            _logger = logger;
            _context = dataContext;
        }

        [HttpGet]
        [Route("GetReceiptImages")]
        public async Task<ReceiptImage[]> GetReceiptImages()
        {
            return await _context.ReceiptImages.ToArrayAsync();
        }

        [HttpGet]
        [Route("GetReceiptImage/{id}")]
        public async Task<ReceiptImage> GetReceiptImage(string id)
        {
            return await _context.ReceiptImages.FirstOrDefaultAsync(x => x.Id.ToString() == id);
        }

        [HttpGet]
        [Route("GetReceiptImageByName/{name}")]
        public async Task<ReceiptImage> GetReceiptImageByName(string name)
        {
            return await _context.ReceiptImages.SingleOrDefaultAsync(x => x.Name == name);
        }

        [HttpPost]
        [Route("AddReceiptImage")]
        public async Task<ReceiptImage> AddReceiptImage([FromBody]Receipt receiptImage)
        {
            ReceiptImage currentReceiptImage = new ReceiptImage()
            {
                Id = Guid.NewGuid(),
                Name = receiptImage.Name,
                CreatedBy = await _context.Users.FirstOrDefaultAsync(),
                ModifiedBy = await _context.Users.FirstOrDefaultAsync(),
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now,
            };

            await _context.ReceiptImages.AddAsync(currentReceiptImage);
            await _context.SaveChangesAsync();
            return currentReceiptImage;
        }

        [HttpPut]
        [Route("UpdateReceiptImage/{id}")]
        public async Task<ReceiptImage> UpdateReceiptImage(string id, [FromBody]ReceiptImage receiptImage)
        {
            ReceiptImage receiptImageReceipt = await _context.ReceiptImages.FirstOrDefaultAsync(x => x.Id == new Guid(id));

            if (receiptImageReceipt != null)
            {
                if (!string.IsNullOrEmpty(receiptImage.Name))
                    receiptImageReceipt.Name = receiptImage.Name;

                receiptImageReceipt.ModifiedBy = await _context.Users.FirstOrDefaultAsync();
                receiptImageReceipt.ModifiedOn = DateTime.Now;

                _context.Update(receiptImageReceipt);
                await _context.SaveChangesAsync();
            }

            return receiptImageReceipt;
        }

        [HttpDelete]
        [Route("DeleteReceiptImage/{id}")]
        public async Task<ReceiptImage> DeleteReceiptImage(string id)
        {
            var receiptImageToDelete = await _context.ReceiptImages.FirstOrDefaultAsync(x => x.Id == new Guid(id));
            _context.ReceiptImages.Remove(receiptImageToDelete);
            await _context.SaveChangesAsync();
            return receiptImageToDelete;
        }


    }
}
