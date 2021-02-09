using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheStorageApp.Shared.Models
{
    public class Category : IModel
    {
        [NotMapped]
        public bool IsSelected { get; set; } = false;
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Int32 Color { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public virtual User CreatedBy { get; set; }
        public Guid CreatedById { get; set; }
        public virtual User ModifiedBy { get; set; }
        public Guid ModifiedById { get; set; }
    }
}
