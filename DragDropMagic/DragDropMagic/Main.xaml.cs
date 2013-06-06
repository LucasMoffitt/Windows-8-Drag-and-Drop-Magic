using System.Linq;
using DragDropMagic.Common;
using DragDropMagic.Model;
using Windows.UI.Xaml.Controls;

namespace DragDropMagic
{
    public sealed partial class Main : LayoutAwarePage
    {
        private MainObject mainObjectContext;

        public Main()
        {
            InitializeComponent();
            mainObjectContext = DemoData.LoadDemoData();
            DataContext = mainObjectContext;
        }

        private void btnNewListItem_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (mainObjectContext == null)
                return;

            mainObjectContext.MyListItems.Add(new MyListItem() {ListHeader = "Tap to Edit Header!"});
        }

        private void btnNewSubListItem_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Button newSubListItemButton = (Button) sender;
            MyListItem currentListItem = (MyListItem) newSubListItemButton.Tag;

            currentListItem.MySubListItems.Add(new MySubListItem());
        }

        private void SubListItems_DragItemsStarting(object sender, DragItemsStartingEventArgs e)
        {
            //The Item we're moving
            e.Data.Properties.Add("sourceItem", e.Items.FirstOrDefault());

            //The List we're moving it from
            ListView selectedList = (ListView) sender;
            e.Data.Properties.Add("sourceList", selectedList.Tag);
        }

        private void SubListItems_DragOver(object sender, Windows.UI.Xaml.DragEventArgs e)
        {
            ListView listViewBelow = (ListView) sender;

            //If the item we're dragging has been draged over another list,
            //remove that before adding the curent one
            if (e.Data.Properties.ContainsKey("destinationList"))
            {
                e.Data.Properties.Remove("destinationList");
            }

            e.Data.Properties.Add("destinationList", listViewBelow.Tag);
        }

        private void SubListItems_Drop(object sender, Windows.UI.Xaml.DragEventArgs e)
        {
            MyListItem sourceList = null;
            MyListItem destinationList = null;
            MySubListItem movingItem = null;

            //The item that's being dragged
            object item;
            if (e.Data.Properties.TryGetValue("sourceItem", out item))
            {
                movingItem = (MySubListItem) item;
            }

            //Source List
            object source;
            if (e.Data.Properties.TryGetValue("sourceList", out source))
            {
                sourceList = (MyListItem) source;
            }

            //Destination List
            object destination;
            if (e.Data.Properties.TryGetValue("destinationList", out destination))
            {
                destinationList = (MyListItem) destination;

            }

            if (movingItem == null || sourceList == null || destinationList == null)
                return;

            if (destination == source)
                return;

            //Remove from source, add to destination.
            destinationList.MySubListItems.Add(movingItem);
            sourceList.MySubListItems.Remove(movingItem);

            e.Handled = true;
        }
 
    }
}
