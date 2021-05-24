using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheStorageApp.Website.ClassAttrinutes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnNameAttribute : Attribute
    {
        private string _columnName;
        public ColumnNameAttribute(string columnName)
        {
            _columnName = columnName;
        }

        public string ColumnName { get { return _columnName; } }
    }
}
