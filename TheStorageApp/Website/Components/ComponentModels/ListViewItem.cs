using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TheStorageApp.Website.ClassAttrinutes;
using TheStorageApp.Website.Models;

namespace TheStorageApp.Website.Components.ComponentModels
{
    public class ListViewItem
    {
        public string Id { get; set; }
        public bool IsSelected { get; set; }
        public bool IsVisible { get; set; }
        public object Item { get; private set; }
        public List<KeyValuePair<string, ListViewItemField>> PropertyValueSet { get; set; }

        public ListViewItem(object item)
        {
            PropertyValueSet = new List<KeyValuePair<string, ListViewItemField>>();
            Item = item;
            Id = ((IModel)item).Id;
            RefreshItemFieldPropertyNames();
        }

        public void RefreshItemFieldPropertyNames()
        {
            PropertyInfo[] properties = Item.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                var pt = property.GetCustomAttributes(typeof(ColumnDataAttribute), false).FirstOrDefault();
                if (pt != null)
                {
                    var colName = ((ColumnDataAttribute)pt).ColumnName;
                    var colDatatype = ((ColumnDataAttribute)pt).ColumnDataType;
                    var value = property.GetValue(Item);
                    
                    ListViewItemField field = new ListViewItemField(value, colDatatype);
                    KeyValuePair<string, ListViewItemField> obj = new KeyValuePair<string, ListViewItemField>(colName, field);
                    PropertyValueSet.Add(obj);
                }
            }
        }

        public static List<string> columnNames(Type type)
        {
            List<string> vals = new List<string>();

            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                var pt = property.GetCustomAttributes(typeof(ColumnDataAttribute), false).FirstOrDefault();
                if (pt != null)
                {
                    var colName = ((ColumnDataAttribute)pt).ColumnName;
                    vals.Add(colName);
                }
            }

            return vals;
        }
    }
}