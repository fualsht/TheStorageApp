using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TheStorageApp.Website.ClassAttrinutes;

namespace TheStorageApp.Website.Components.ComponentModels
{
    public class ListViewComponentBase: ComponentBase
    {
        public ListViewComponentBase()
        {
            string s = "";
        }

        [Parameter]
        public string Name { get; set; } = "";

        public List<ListViewItem> Items { get; set; } = new List<ListViewItem>();
        public string[] ColumnNames { get; set; } = new string[0];

        public void LoadItems<T>(T[] items)
        {
            Items = new List<ListViewItem>();
            ColumnNames = columnNames(typeof(T)).ToArray();

            foreach (var item in items)
            {
                Items.Add(new ListViewItem(item));
            }
            StateHasChanged();
        }

        public void Refresh()
        {
            StateHasChanged();
        }

        public void OnRowClick(MouseEventArgs e, ListViewItem item)
        {
            if (e.AltKey || e.ShiftKey)
            {
                if (e.ShiftKey)
                {
                    int startIndex = Items.IndexOf(Items.FirstOrDefault(x => x.IsSelected));
                    int endIndex = Items.IndexOf(item);
                    if (startIndex <= endIndex)
                    {
                        SelectRange(startIndex, endIndex);
                    }
                    else
                    {
                        SelectRange(endIndex + 1, startIndex + 1);
                    }
                }
                ToggleSelect(item);
            }
            else
            {
                Select(item);
            }
        }

        public void SelectRange(int start, int end)
        {
            foreach (var item in Items)
            {
                item.IsSelected = false;
            }
            for (int i = start; i < end; i++)
            {
                Items[i].IsSelected = true;
            }
        }

        public void Select(ListViewItem model)
        {
            foreach (var item in Items)
            {
                item.IsSelected = false;
            }
            model.IsSelected = true;
        }

        public void ToggleSelect(ListViewItem item)
        {
            item.IsSelected = !item.IsSelected;
        }

        public string[] GetValueMetaData(Type type)
        {
            List<string> values = new List<string>();
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                var name = property.Name;
                values.Add(name);
            }
            return values.ToArray();
        }

        public List<string> columnNames(Type type)
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
