using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheStorageApp.Website.Models
{
    public class Model : IModel
    {
        public bool IsSelected { get; set; } = false;
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
        public AppUser CreatedBy { get; set; }
        public string ModifiedById { get; set; }
        public DateTime ModifiedOn { get; set; }
        public AppUser ModifiedBy { get; set; }
        public Field[] Fields { get; set; }
        public ModelRelationship[] SourceModelRelationships { get; set; }
        public ModelRelationship[] RelatedModelRelationships { get; set; }
        public Model()
        {
            //Fields = new Field[0];
            //SourceModelRelationships = new ModelRelationship[0];
            //RelatedModelRelationships = new ModelRelationship[0];
        }
    }
}
