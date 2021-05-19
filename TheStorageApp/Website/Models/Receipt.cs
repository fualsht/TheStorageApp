using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheStorageApp.Website.Models
{
    public class Receipt : IModel
    {
        [NotMapped]
        public bool IsSelected { get; set; } = false;
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        public virtual AppUser CreatedBy { get; set; }
        public string CreatedById { get; set; }

        public virtual AppUser ModifiedBy { get; set; }
        public string ModifiedById { get; set; }

        public string ReceiptHolderId { get; set; }
        public virtual AppUser ReceiptHolder { get; set; }

        public virtual Shop Shop { get; set; }
        public string ShopId { get; set; }

        public virtual Category Category { get; set; }
        public string CategoryId { get; set; }

        public virtual Category[] Categories { get; set; }
        public virtual ReceiptImage[] RecipetImages { get; set; }
        public virtual Tag[] Tags { get; set; }
    }
}
