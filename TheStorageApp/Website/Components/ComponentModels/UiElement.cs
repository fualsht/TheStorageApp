using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheStorageApp.Website.Components
{
    public class UiElement : IUiElement
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }
        public object Item { get; set; }
    }
}
