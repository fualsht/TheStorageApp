using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheStorageApp.Website.Components
{
    public partial class TabControlComponent : ComponentBase
    {
        [Parameter]
        public TabControlCollection TabCollection { get; set; }
        [Parameter]
        public EventCallback<TabSelectEventArgs> OnTabSelectCallBack { get; set; }

        public TabControlComponent()
        {
            TabCollection = new TabControlCollection();
        }

        public void OnUiElementClick(MouseEventArgs args, UiElement item)
        {
            foreach (var tab in TabCollection)
                tab.IsSelected = false;
            item.IsSelected = true;
            
            TabSelectEventArgs tabControlComponent = new TabSelectEventArgs(item, TabCollection.IndexOf(item), args);
            OnTabSelectCallBack.InvokeAsync(tabControlComponent);
        }
    }

    public class TabSelectEventArgs
    {
        UiElement _SelectedItem;
        public UiElement SelectedItem { get { return _SelectedItem; } }

        MouseEventArgs _MouseEventArgs;
        public MouseEventArgs MouseEventArgs { get { return _MouseEventArgs; } }

        int _SelectedIndex;
        public int SelectedIndex { get { return _SelectedIndex; } }

        public TabSelectEventArgs(UiElement selectedItem, int index, MouseEventArgs mouseEventArgs)
        {
            _SelectedItem = selectedItem;
            _MouseEventArgs = mouseEventArgs;
            _SelectedIndex = index;
        }
    }

    public class TabControlCollection : IList<UiElement>
    {
        protected List<UiElement> Items;
        public UiElement this[int index] { get { return this.Items[index]; } set { Items[index] = value; } }

        public int Count { get { return Items.Count; } }

        public bool IsReadOnly { get { return false; } }

        public TabControlCollection()
        {
            Items = new List<UiElement>();
        }

        public void Add(UiElement item)
        {
            Items.Add(item);
        }

        public void Add(string name, bool isSelected = false, object item = null)
        {
            UiElement element = new UiElement();
            element.Id = Guid.NewGuid();
            element.Name = name;
            element.IsSelected = isSelected;
            element.Item = item;
            Items.Add(element);
        }

        public void Clear()
        {
            Items.Clear();
        }

        public bool Contains(UiElement item)
        {
            return Items.Contains(item);
        }

        public void CopyTo(UiElement[] array, int arrayIndex)
        {
            Items.CopyTo(array, arrayIndex);
        }

        public IEnumerator<UiElement> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        public int IndexOf(UiElement item)
        {
            return Items.IndexOf(item);
        }

        public void Insert(int index, UiElement item)
        {
            Items.Insert(index, item);
        }

        public bool Remove(UiElement item)
        {
            return Items.Remove(item);
        }

        public void RemoveAt(int index)
        {
            Items.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Items.GetEnumerator();
        }
    }
}
