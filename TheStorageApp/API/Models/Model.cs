using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheStorageApp.API.Models
{
    public class Model : IModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string PluralName { get; set; }
        public string Description { get; set; }
        public byte[] SmallImage { get; set; }
        public byte[] LargeImage { get; set; }
        public string PrimaryColor { get; set; }
        public string SecondaryColor { get; set; }
        public string CreatedById { get; set; }
        public DateTime CreatedOn { get; set; }
        public virtual AppUser CreatedBy { get; set; }
        public string ModifiedById { get; set; }
        public DateTime ModifiedOn { get; set; }
        public virtual AppUser ModifiedBy { get; set; }
        public virtual ICollection<Field> Fields { get; set; }
        //public string FieldLookupModelId { get; set; }
        //public virtual Model FieldLookupModel { get; set; }
        public virtual ICollection<Field> ModelFieldsLookup { get; set; }
        public virtual ICollection<ModelRelationship> SourceModelRelationships { get; set; }
        public virtual ICollection<ModelRelationship> RelatedModelRelationships { get; set; }
    }
}
