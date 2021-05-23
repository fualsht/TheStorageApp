using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheStorageApp.Website.Models
{
    public class ModelViewViewModel : IModel
    {
        public bool IsSelected { get; set; } = false;
        public string Id { get; set; }
        public string Name { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public List<Field> Fields { get; set; }
        public string CreatedById { get; set; }
        public DateTime CreatedOn { get; set; }
        public AppUser CreatedBy { get; set; }
        public string ModifiedById { get; set; }
        public DateTime ModifiedOn { get; set; }
        public AppUser ModifiedBy { get; set; }
    }
}
