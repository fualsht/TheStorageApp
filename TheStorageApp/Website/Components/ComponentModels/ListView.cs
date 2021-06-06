using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheStorageApp.Website.Components.ComponentModels
{
    public class ListView
    {
        public string Name { get; set; }
        public List<ListViewItem> Items { get; set; }

        public ListView(string name, object[] items)
        {
            Name = name;
            Items = new List<ListViewItem>();

            foreach (var item in items)
            {
                Items.Add(new ListViewItem(item));
            }
        }
    }
}
