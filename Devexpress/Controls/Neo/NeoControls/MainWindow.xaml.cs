using NeoTrader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Bars;
using System.Globalization;
using NeoTrader.UI.Controls;
using DevExpress.Xpf.Grid.TreeList;
using DevExpress.Xpf.Grid;
using System.ComponentModel;
using System.Collections;
using System.Collections.ObjectModel;
using NeoControls.View;
using NeoTrader.UI.ViewModels;

namespace NeoControls
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = ViewModelSource.Create<MainWindowVm>();
            InitializeComponent();
            //CompatibilitySettings.UseLightweightBarItems = false;
        }

        private void ToolBarControl_Loaded(object sender, RoutedEventArgs e)
        {
            ToolBarControl toolBarControl = (ToolBarControl)sender;
            List<Border> borders = NeoTrader.UiUtils.UIUtils.GetChildObjects<Border>(toolBarControl, "PART_BAR_Border");
            double maxVal = borders[0].ActualWidth;
            //foreach (Border b in borders)
            //{
            //    if(b.ActualWidth > maxVal)
            //        maxVal = b.ActualWidth;
            //}

            //foreach (var b in borders)
            //{
            //    b.Width = maxVal;
            //}
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Window w = new Window()
            {
                Name = "TableTool"
            };

            w.Content = new TableToolsDemo();
            w.Show();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Window w = new Window()
            {
                Name = "单个Tools"
            };

            w.Content = new WorkSpaceTools();
            w.Show();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            Window w = new Window() 
            {
                Name = "单个Tools"
            };

            w.Content = new SiginTools();
            w.Show();
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            Window w = new Window()
            {
                Name = "TreeListView"
            };

            w.Content = new TreeListViewDemo();
            w.Show();
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            Window w = new Window()
            {
                Name = "DragDrop"
            };

            w.Content = new DropDragTest();
            w.Show();
        }
    }

  
    }
