using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheStorageApp.Shared.Models
{
    public class ReceiptImage : IModel
    {
        [NotMapped]
        public bool IsSelected { get; set; } = false;
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public virtual User CreatedBy { get; set; }
        public Guid CreatedById { get; set; }
        public virtual User ModifiedBy { get; set; }
        public Guid ModifiedById { get; set; }
        public byte[] Image { get; set; }
        public virtual Receipt Receipt { get; set; }
        public virtual Guid ReceiptId { get; set; }

    }
}
