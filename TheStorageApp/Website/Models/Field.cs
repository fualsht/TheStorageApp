using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheStorageApp.Website.ClassAttrinutes;

namespace TheStorageApp.Website.Models
{
    public enum DataType { Text, MultiLineText, Interger, Decimal, Currency, Option, MultiSelectOptionSet, LookUp, DateTime, Image, PrimaryKey, ForeignKey }
    public class Field : IModel
    {
        public string Id { get; set; }
        [ColumnData("Name", FieldDataTypes.Text)]
        public string Name { get; set; }
        [ColumnData("Description", FieldDataTypes.Text)]
        public string Description { get; set; }
        public string CreatedById { get; set; }
        [ColumnData("Created On", FieldDataTypes.DateTime)]
        public DateTime CreatedOn { get; set; }
        public AppUser CreatedBy { get; set; }
        public string ModifiedById { get; set; }
        [ColumnData("Modified On", FieldDataTypes.DateTime)]
        public DateTime ModifiedOn { get; set; }
        public AppUser ModifiedBy { get; set; }
        public string DataTypeId { get; set; }
        public DataType DataType { get; set; }
        [ColumnData("Min Size", FieldDataTypes.Integer)]
        public int MinSize { get; set; }
        [ColumnData("Min Size", FieldDataTypes.Integer)]
        public int MaxSize { get; set; }
        [ColumnData("Unique", FieldDataTypes.Bool)]
        public bool Unique { get; set; }
        [ColumnData("Unique", FieldDataTypes.Bool)]
        public bool Requiered { get; set; }
        public string ModelId { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public Model Model { get; set; }
        public bool IsSelected { get; set; }
        public ModelRelationship[] SourceModelRelationships { get; set; }
        public ModelRelationship[] RelatedModelRelationships { get; set; }

        public Field()
        {
            DataType = DataType.Text;
        }

        public Field(string name)
        {
            DataType = DataType.Text;
            Name = name;
        }

        public static Field NewField(string modelId)
        {
            Field newField = new Field();

            newField.DataType = DataType.Text;
            newField.ModelId = modelId;

            return newField;
        }
    }
}
