using System.Collections.ObjectModel;

namespace DragDropMagic.Model
{
    public class MainObject
    {
        public string FancyTitle { get; set; }
        public ObservableCollection<MyListItem> MyListItems { get; set; }
        
        public MainObject()
        {
            FancyTitle = "We're not binding this so no one will see it. Good stuff is below.";
            MyListItems = new ObservableCollection<MyListItem>();
        }
    }
}
