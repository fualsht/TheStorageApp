using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheStorageApp.Shared.Models
{
    public class User : IModel
    {
        [NotMapped]
        public bool IsSelected { get; set; } = false;
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public virtual IEnumerable<Receipt> Receipts { get; set; }
    }
}
