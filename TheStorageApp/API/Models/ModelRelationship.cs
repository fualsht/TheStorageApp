using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheStorageApp.API.Models
{
    public class ModelRelationship : IModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreatedById { get; set; }
        public DateTime CreatedOn { get; set; }
        public AppUser CreatedBy { get; set; }
        public string ModifiedById { get; set; }
        public DateTime ModifiedOn { get; set; }
        public AppUser ModifiedBy { get; set; }
        public string SorceModelId { get; set; }
        public Model SorceModel { get; set; }
        public string RelatedModelId { get; set; }
        public Model RelatedModel { get; set; }
        public string SourceFieldId { get; set; }
        public Field SourceField { get; set; }
        public string RelatedFieldId { get; set; }
        public Field RelatedField { get; set; }
    }
}
