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
            CompatibilitySettings.UseLightweightBarItems = false;
        }

        private void ToolBarControl_Loaded(object sender, RoutedEventArgs e)
        {
            ToolBarControl toolBarControl = (ToolBarControl)sender;
            List<Border> borders = NeoTrader.UiUtils.UIUtils.GetChildObjects<Border>(toolBarControl, "PATA_BAR_Border");
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
    }

    public class BarItemDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate SubItemNormalTemplate { get; set; }
        public DataTemplate SubItemLinkTemplate { get; set; }
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item == null || !(item is CommandVm))
                return base.SelectTemplate(item, container);

            var vm = item as CommandVm;
            return vm.Commands == null ? SubItemNormalTemplate : SubItemLinkTemplate;
        }
    }
}
