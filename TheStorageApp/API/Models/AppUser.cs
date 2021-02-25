using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheStorageApp.API.Models
{
    public class AppUser : IdentityUser
    {


        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual IEnumerable<Receipt> Receipts { get; set; }

        public virtual IEnumerable<Category> CreatedByCategories { get; set; }
        public virtual IEnumerable<Category> ModifiedByCategories { get; set; }

        public virtual IEnumerable<Receipt> CreatedByReceipts { get; set; }
        public virtual IEnumerable<Receipt> ModifiedByReceipts { get; set; }

        public virtual IEnumerable<LogEntry> CreatedByLogEntries { get; set; }

        public virtual IEnumerable<ReceiptImage> CreatedByReceiptImages { get; set; }
        public virtual IEnumerable<ReceiptImage> ModifiedByReceiptImages { get; set; }

        public virtual IEnumerable<Shop> CreatedByShops { get; set; }
        public virtual IEnumerable<Shop> ModifiedByShops { get; set; }

        public virtual IEnumerable<Tag> CreatedByTags { get; set; }
        public virtual IEnumerable<Tag> ModifiedByTags { get; set; }

    }
}
