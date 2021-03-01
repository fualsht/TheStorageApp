using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheStorageApp.Website.Models
{
    public class Category : IModel
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
    }
}
