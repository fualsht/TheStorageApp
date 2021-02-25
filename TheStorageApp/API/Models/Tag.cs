using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TheStorageApp.API.Models
{
    public class Tag : IModel
    {
        [NotMapped]
        public bool IsSelected { get; set; } = false;
        public string Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public virtual AppUser CreatedBy { get; set; }
        public string CreatedById { get; set; }
        public virtual AppUser ModifiedBy { get; set; }
        public string ModifiedById { get; set; }
        public virtual Receipt[] Receipts { get; set; }
    }
}
