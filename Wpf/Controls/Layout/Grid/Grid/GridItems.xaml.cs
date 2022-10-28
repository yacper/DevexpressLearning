using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Grid
{
public class Item
{
    public     string Val { get; set; } = "hello";
        public int    Row { get; set; }
        public int    Column { get; set; }

}

    /// <summary>
    /// GridItems.xaml 的交互逻辑
    /// </summary>
    public partial class GridItems : Window
    {
        public GridItems()
        {
            this.DataContext = this;
            InitializeComponent();

      
        }

        private void Items_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (Items.Count <= 3)
            {
                var def = new RowDefinition() { Height   = new GridLength(1, GridUnitType.Star), MinHeight = 100 };
                ItemsHost.RowDefinitions.Add(def);
                ItemsHost.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star), MinWidth  = 100 });
                ItemsHost.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star), MinWidth  = 100 });
                ItemsHost.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star), MinWidth  = 100 });

                System.Windows.Controls.Grid.SetColumn(ItemsHost.Children[0], 0);
                System.Windows.Controls.Grid.SetColumn(ItemsHost.Children[1], 1);
                System.Windows.Controls.Grid.SetColumn(ItemsHost.Children[2], 2);


            }
        }

        public ObservableCollection<Item> Items { get; set; } = new() { new Item(){Row = 0, Column = 0}, new Item(){Val = "world", Row = 0, Column = 1} };


        protected System.Windows.Controls.Grid ItemsHost
        {
            get
            {
                return (System.Windows.Controls.Grid)typeof(MultiSelector).InvokeMember("ItemsHost",
                                                                 BindingFlags.NonPublic | BindingFlags.GetProperty | BindingFlags.Instance,
                                                                 null, im, null);
            }
        }

private Panel GetItemsPanel(DependencyObject itemsControl)
{
    ItemsPresenter itemsPresenter = GetVisualChild<ItemsPresenter>(itemsControl);
    Panel itemsPanel = VisualTreeHelper.GetChild(itemsPresenter, 0) as Panel;
    return itemsPanel;
}

private static T GetVisualChild<T>(DependencyObject parent) where T : Visual
{
    T child = default(T);

    int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
    for (int i = 0; i < numVisuals; i++)
    {
        Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
        child = v as T;
        if (child == null)
        {
            child = GetVisualChild<T>(v);
        }
        if (child != null)
        {
            break;
        }
    }
    return child;
}

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Items.CollectionChanged += Items_CollectionChanged;

            Items.Add(new Item() { Val = "ok", Row = 0, Column = 2});

        }
    }
}
