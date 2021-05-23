using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheStorageApp.Website.Models
{
    public enum DataTypes { Text, MultiLineText, Interger, Decimal, Currency, Option, MultiSelectOptionSet, LookUp, DateTime, Image }
    public class Field : IModel
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
        public string DataTypeId { get; set; }
        public DataTypes DataType { get; set; }
        public int MinSize { get; set; }
        public int MaxSize { get; set; }
        public bool Unique { get; set; }
        public bool Requiered { get; set; }
        public string ModelId { get; set; }
        public Model Model { get; set; }
        public bool IsSelected { get; set; }
        public ModelRelationship[] SourceModelRelationships { get; set; }
        public ModelRelationship[] RelatedModelRelationships { get; set; }
    }
}
