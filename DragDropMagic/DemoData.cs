using System.Collections.ObjectModel;
using DragDropMagic.Model;

namespace DragDropMagic
{
    public static class DemoData
    {
        public static MainObject LoadDemoData ()
        {
            var listItem1 = new MyListItem
                {
                    ListHeader = "List #1",
                    MySubListItems = new ObservableCollection<MySubListItem> 
                        {
                            new MySubListItem  {Item = "List #1 - Item #1"},
                            new MySubListItem  {Item = "List #1 - Item #2"},
                            new MySubListItem  {Item = "List #1 - Item #3"}
                        }
                };

            var listItem2 = new MyListItem
            {
                ListHeader = "List #2",
                MySubListItems = new ObservableCollection<MySubListItem> 
                        {
                            new MySubListItem  {Item = "List #2 - Item #1"},
                            new MySubListItem  {Item = "List #2 - Item #2"},
                            new MySubListItem  {Item = "List #2 - Item #3"}
                        }
            };

            var listItem3 = new MyListItem
            {
                ListHeader = "List #3",
                MySubListItems = new ObservableCollection<MySubListItem> 
                        {
                            new MySubListItem  {Item = "List #3 - Item #1"},
                            new MySubListItem  {Item = "List #3 - Item #2"},
                            new MySubListItem  {Item = "List #3 - Item #3"}
                        }
            };


            var demoData = new MainObject
                {
                    FancyTitle = "Some Demo Thing, WHAT OF IT!?",
                    MyListItems = new ObservableCollection<MyListItem> 
                        {
                            listItem1,
                            listItem2,
                            listItem3
                        }
                };

            return demoData;
        }
    }
}
