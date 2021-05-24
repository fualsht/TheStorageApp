using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TheStorageApp.Website.ClassAttrinutes;

namespace TheStorageApp.Website.Components.ComponentModels
{
    public class ListViewItem
    {
        public string Id { get; set; }
        public bool IsSelected { get; set; }
        public bool IsVisible { get; set; }
        public object Item { get; set; }

        public ListViewItem(object item)
        {
            Item = item;
        }

        public string[] ItemFieldPropertyNames
        {
            get
            {
                List<string> PropertyNames = new List<string>();
                PropertyInfo[] properties = Item.GetType().GetProperties();
                foreach (System.Reflection.PropertyInfo property in properties)
                {
                    var pt = property.GetCustomAttributes(typeof(ColumnNameAttribute), false).FirstOrDefault();
                    if (pt != null)
                    {
                        var colName = ((ColumnNameAttribute)pt).ColumnName;
                        PropertyNames.Add(colName);
                    }
                }
                return PropertyNames.ToArray();
            }
        }
    }
}