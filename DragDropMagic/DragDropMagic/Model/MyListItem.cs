using System.Collections.ObjectModel;

namespace DragDropMagic.Model
{
    public class MyListItem
    {
        public ObservableCollection<MySubListItem> MySubListItems { get; set; }
        public string ListHeader { get; set; }

        public MyListItem()
        {
            MySubListItems = new ObservableCollection<MySubListItem>();
            ListHeader = string.Empty;
        }
    }
}
