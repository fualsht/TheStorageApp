using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheStorageApp.Website.ClassAttrinutes;

namespace TheStorageApp.Website.Models
{
    public class Model : IModel
    {
        public bool IsSelected { get; set; } = false;
        public string Id { get; set; }

        [ColumnData("Name", FieldDataTypes.Text)]
        public string Name { get; set; }

        [ColumnData("PluralName", FieldDataTypes.Text)]
        public string PluralName { get; set; }

        [ColumnData("Description", FieldDataTypes.Text)]
        public string Description { get; set; }

        [ColumnData("SmallImage", FieldDataTypes.Image)]
        public byte[] SmallImage { get; set; }

        [ColumnData("LargeImage", FieldDataTypes.Image)]
        public byte[] LargeImage { get; set; }

        [ColumnData("PrimaryColor", FieldDataTypes.Color)]
        public string PrimaryColor { get; set; }

        [ColumnData("SecondaryColor", FieldDataTypes.Color)]
        public string SecondaryColor { get; set; }

        public string CreatedById { get; set; }
        public DateTime CreatedOn { get; set; }
        public AppUser CreatedBy { get; set; }
        public string ModifiedById { get; set; }
        public DateTime ModifiedOn { get; set; }
        public AppUser ModifiedBy { get; set; }
        public ICollection<Field> Fields { get; set; }
        public ICollection<ModelRelationship> SourceModelRelationships { get; set; }
        public ICollection<ModelRelationship> RelatedModelRelationships { get; set; }
        public Model()
        {
            Fields = new Field[0];
            SourceModelRelationships = new ModelRelationship[0];
            RelatedModelRelationships = new ModelRelationship[0];
        }
    }
}
