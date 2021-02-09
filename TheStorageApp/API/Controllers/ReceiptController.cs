using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using TheStorageApp.API.Data;
using TheStorageApp.Shared.Models;

namespace TheStorageApp.API.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class ReceiptController : ControllerBase
    {
        private readonly ILogger<ReceiptController> _logger;
        private readonly DataContext _context;

        public ReceiptController(ILogger<ReceiptController> logger, DataContext dataContext)
        {
            _logger = logger;
            _context = dataContext;
        }

        [HttpGet]
        [Route("GetReceipts")]
        public async Task<Receipt[]> GetReceipts()
        {
            return await _context.Receipts.
                Include(x => x.ModifiedBy).
                Include(x => x.CreatedBy).
                Include(x => x.Category).
                Include(x => x.Tags).
                Include(x => x.Shop).
                ToArrayAsync();
        }

        [HttpGet]
        [Route("GetReceipt/{id}")]
        public async Task<Receipt> GetReceipt(string id)
        {
            return await _context.Receipts.
                Include(x => x.ModifiedBy).
                Include(x => x.CreatedBy).
                Include(x => x.Category).
                Include(x => x.Tags).
                Include(x => x.Shop).
                SingleOrDefaultAsync(x => x.Id.ToString() == id);
        }

        [HttpGet]
        [Route("GetReceiptByName/{name}")]
        public async Task<Receipt> GetReceiptByName(string name)
        {
            return await _context.Receipts.
                Include(x => x.ModifiedBy).
                Include(x => x.CreatedBy).
                Include(x => x.Category).
                Include(x => x.Tags).
                Include(x => x.Shop).
                SingleOrDefaultAsync(x => x.Name == name);
        }

        [HttpPost]
        [Route("AddReceipt")]
        public async Task<Receipt> AddReceipt([FromBody] Receipt receipt)
        {
            Receipt newreceipt = new Receipt
            {
                Id = Guid.NewGuid(),
                Name = receipt.Name,
                Category = receipt.Category,
                ReceiptHolder = receipt.ReceiptHolder,
                Shop = receipt.Shop,
                RecipetImages = receipt.RecipetImages,
                CreatedBy = _context.Users.FirstOrDefault(),
                ModifiedBy = _context.Users.FirstOrDefault(),
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now
            };

            await _context.Receipts.AddAsync(newreceipt);
            await _context.SaveChangesAsync();
            return newreceipt;
        }

        [HttpPut]
        [Route("UpdateReceipt/{id}")]
        public async Task<Receipt> UpdateReceipt(string id, [FromBody]Receipt receipt)
        {
            Receipt currentReceipt = _context.Receipts.
                Include(x => x.ModifiedBy).
                Include(x => x.CreatedBy).
                Include(x => x.Category).
                Include(x => x.Tags).
                Include(x => x.Shop).FirstOrDefault(x => x.Id == new Guid(id));

            if (currentReceipt != null)
            {
                if (!string.IsNullOrEmpty(receipt.Name))
                    currentReceipt.Name = receipt.Name;

                if (receipt.ReceiptHolder != null)
                    currentReceipt.ReceiptHolder = receipt.ReceiptHolder;

                if (receipt.Shop != null)
                    currentReceipt.Shop = receipt.Shop;

                if (receipt.Category != null)
                    currentReceipt.Category = receipt.Category;

                currentReceipt.ModifiedBy = await _context.Users.FirstOrDefaultAsync();
                currentReceipt.ModifiedOn = DateTime.Now;

                _context.Update(currentReceipt);
                await _context.SaveChangesAsync();
            }

            return currentReceipt;
        }

        [HttpDelete]
        [Route("DeleteReceipt/{id}")]
        public async Task<Receipt> DeleteReceipt(string id)
        {
            var receiptToDelete =  await _context.Receipts.FirstOrDefaultAsync(x => x.Id == new Guid(id));
            _context.Receipts.Remove(receiptToDelete);
            await _context.SaveChangesAsync();
            return receiptToDelete;
        }
    }
}
