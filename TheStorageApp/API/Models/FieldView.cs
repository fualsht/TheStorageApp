using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheStorageApp.API.Models
{
    public class FieldView : IModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] SmallImage { get; set; }
        public byte[] LargeImage { get; set; }
        public ModelViewTypes ViewType { get; set; }
        public string CreatedById { get; set; }
        public DateTime CreatedOn { get; set; }
        public AppUser CreatedBy { get; set; }
        public string ModifiedById { get; set; }
        public DateTime ModifiedOn { get; set; }
        public AppUser ModifiedBy { get; set; }
        public FieldView[] Fields { get; set; }
    }
}
