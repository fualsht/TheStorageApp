using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TheStorageApp.Shared.Models
{
    public class Tag : IModel
    {
        [NotMapped]
        public bool IsSelected { get; set; } = false;
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public virtual User CreatedBy { get; set; }
        public Guid CreatedById { get; set; }
        public virtual User ModifiedBy { get; set; }
        public Guid ModifiedById { get; set; }
        public virtual Receipt[] Receipts { get; set; }
    }
}
