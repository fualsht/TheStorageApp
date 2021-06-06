using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheStorageApp.Website.ClassAttrinutes
{
    public enum FieldDataTypes { Text, Image, Color, Number, Money, Bool, DateTime, Integer, PrimaryKey }
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnDataAttribute : Attribute
    {
        private string _columnName;
        public string ColumnName { get { return _columnName; } }

        public FieldDataTypes _columnDataType;
        public FieldDataTypes ColumnDataType { get { return _columnDataType; } }

        public ColumnDataAttribute(string columnName = "", FieldDataTypes datatype = FieldDataTypes.Text)
        {
            _columnName = columnName;
            _columnDataType = datatype;
        }

    }
}
