using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheStorageApp.API.Data;
using TheStorageApp.API.Models;

namespace TheStorageApp.API.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class ReceiptController : APIControllerBase<Receipt>
    {

        public ReceiptController(ILogger<ReceiptController> logger, DataContext dataContext, IHttpContextAccessor httpContextAccessor) : base(logger, dataContext, httpContextAccessor)
        {
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Route("GetReceipt")]
        public async Task<ActionResult<Receipt[]>> GetReceipts()
        {
            try
            {
                return await _dataContext.Receipts.
                Include(x => x.ModifiedBy).
                Include(x => x.CreatedBy).
                Include(x => x.Category).
                Include(x => x.Tags).
                Include(x => x.Shop).
                ToArrayAsync();
            }
            catch (Exception exception)
            {
                return this.InternalServerError(exception, "Error getting receipts");
            }
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Route("GetReceipt/{id}")]
        public async Task<ActionResult<Receipt>> GetReceipt(string id)
        {
            try
            {
                return await _dataContext.Receipts.
                   Include(x => x.ModifiedBy).
                   Include(x => x.CreatedBy).
                   Include(x => x.Category).
                   Include(x => x.Tags).
                   Include(x => x.Shop).
                   SingleOrDefaultAsync();
            }
            catch (Exception exception)
            {
                return this.InternalServerError(exception, "Error getting receipt with ID: " + id);
            }
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Route("GetReceiptByName/{name}")]
        public async Task<ActionResult<Receipt>> GetReceiptByName(string name)
        {
            try
            {
                return await _dataContext.Receipts.
                    Include(x => x.ModifiedBy).
                    Include(x => x.CreatedBy).
                    Include(x => x.Category).
                    Include(x => x.Tags).
                    Include(x => x.Shop).
                    SingleOrDefaultAsync(x => x.Name == name);
            }
            catch (Exception exception)
            {
                return this.InternalServerError(exception, "Error getting receipt with Name: " + name);
            }
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Route("AddReceipt")]
        public async Task<ActionResult<Receipt>> AddReceipt([FromBody] Receipt receipt)
        {
            try
            {
                Receipt newReceipt = new Receipt()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = receipt.Name,
                    Category = receipt.Category,
                    ReceiptHolder = receipt.ReceiptHolder,
                    Shop = receipt.Shop,
                    RecipetImages = receipt.RecipetImages,
                    CreatedBy = _dataContext.Users.FirstOrDefault(),
                    ModifiedBy = _dataContext.Users.FirstOrDefault(),
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now
                };
                await _dataContext.Receipts.AddAsync(newReceipt);
                await _dataContext.SaveChangesAsync();
                return newReceipt;
            }
            catch (Exception exception)
            {
                return this.InternalServerError(exception, "Error creating category with Id: " + receipt.Id);
            }
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Route("AddReceipts")]
        public async Task<ActionResult<Receipt[]>> AddReceipts([FromBody] Receipt[] receipts)
        {
            try
            {
                List<Receipt> newReceipts = new List<Receipt>();

                foreach (var receipt in receipts)
                {
                    Receipt newReceipt = new Receipt()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = receipt.Name,
                        Category = receipt.Category,
                        ReceiptHolder = receipt.ReceiptHolder,
                        Shop = receipt.Shop,
                        RecipetImages = receipt.RecipetImages,
                        CreatedBy = _dataContext.Users.FirstOrDefault(),
                        ModifiedBy = _dataContext.Users.FirstOrDefault(),
                        CreatedOn = DateTime.Now,
                        ModifiedOn = DateTime.Now
                    };
                    newReceipts.Add(newReceipt);
                }

                await _dataContext.Receipts.AddRangeAsync(newReceipts.ToArray());
                await _dataContext.SaveChangesAsync();

                return newReceipts.ToArray();
            }
            catch (Exception exception)
            {
                return this.InternalServerError(exception, "Error creating " + receipts.Length.ToString() + " receipts");
            }
        }



        [HttpPut]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Route("UpdateReceipt")]
        public async Task<ActionResult<Receipt>> UpdateReceipt([FromBody] Receipt receipt)
        {
            try
            {
                Receipt currentReceipt = _dataContext.Receipts.
                Include(x => x.ModifiedBy).
                Include(x => x.CreatedBy).
                Include(x => x.Category).
                Include(x => x.Tags).
                Include(x => x.Shop).FirstOrDefault(x => x.Id == receipt.Id);

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

                    currentReceipt.ModifiedBy = await _dataContext.Users.FirstOrDefaultAsync();
                    currentReceipt.ModifiedOn = DateTime.Now;

                    _dataContext.Update(currentReceipt);
                    await _dataContext.SaveChangesAsync();
                }

                return currentReceipt;
            }
            catch (Exception exception)
            {
                return this.InternalServerError(exception, "Error updating receipt with Id: " + receipt.Id);
            }
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Route("UpdateReceipts")]
        public async Task<ActionResult<Receipt[]>> UpdateReceipts([FromBody] Receipt[] receipts)
        {
            try
            {
                List<Receipt> updatedReceipts = new List<Receipt>();

                foreach (var receipt in receipts)
                {
                    Receipt currentReceipt = _dataContext.Receipts.
                          Include(x => x.ModifiedBy).
                          Include(x => x.CreatedBy).
                          Include(x => x.Category).
                          Include(x => x.Tags).
                          Include(x => x.Shop).FirstOrDefault(x => x.Id == receipt.Id);

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

                        currentReceipt.ModifiedBy = await _dataContext.Users.FirstOrDefaultAsync();
                        currentReceipt.ModifiedOn = DateTime.Now;

                        updatedReceipts.Add(currentReceipt);
                    }
                }

                _dataContext.UpdateRange(updatedReceipts);
                await _dataContext.SaveChangesAsync();

                return updatedReceipts.ToArray();
            }
            catch (Exception exception)
            {
                return this.InternalServerError(exception, "Error updating " + receipts.Length.ToString() + " receipts");
            }
        }

        [HttpDelete]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Route("DeleteReceipt/{id}")]
        public async Task<ActionResult<Receipt>> DeleteReceipt(string id)
        {
            try
            {
                var ReceiptToDelete = await _dataContext.Receipts.FirstOrDefaultAsync(x => x.Id == id);

                _dataContext.Receipts.Remove(ReceiptToDelete);
                await _dataContext.SaveChangesAsync();
                return ReceiptToDelete;
            }
            catch (Exception exception)
            {
                return this.InternalServerError(exception, "Error deleting receipt with Id: " + id);
            }
        }

        [HttpDelete]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Route("DeleteReceipts")]
        public async Task<ActionResult<Receipt[]>> DeleteReceipts([FromBody] string[] ids)
        {
            try
            {
                List<Receipt> Receipts = new List<Receipt>();

                foreach (var id in ids)
                {
                    Receipts.Add(await _dataContext.Receipts.FirstOrDefaultAsync(x => x.Id == id));
                }

                _dataContext.Receipts.RemoveRange(Receipts);

                await _dataContext.SaveChangesAsync();

                return Ok(Receipts.ToArray());
            }
            catch (Exception exception)
            {
                return this.InternalServerError(exception, "Error deleting " + ids.Length.ToString() + " receipts");
            }
        }
    }
}









