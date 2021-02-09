using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheStorageApp.Shared.Models
{
    public class Receipt : IModel
    {
        [NotMapped]
        public bool IsSelected { get; set; } = false;
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        public virtual User CreatedBy { get; set; }
        public Guid CreatedById { get; set; }

        public virtual User ModifiedBy { get; set; }
        public Guid ModifiedById { get; set; }

        public Guid ReceiptHolderId { get; set; }
        public virtual User ReceiptHolder { get; set; }

        public virtual Shop Shop { get; set; }
        public Guid ShopId { get; set; }

        public virtual Category Category { get; set; }
        public Guid CategoryId { get; set; }

        public virtual Category[] Categories { get; set; }
        public virtual ReceiptImage[] RecipetImages { get; set; }
        public virtual Tag[] Tags { get; set; }
    }
}
