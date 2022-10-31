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

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

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
    }

    public class BarItemDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate BarCheckItemTemplate { get; set; }
        public DataTemplate BarButtonItemTemplate { get; set; }
        public DataTemplate BarSubItemTemplate { get; set; }
        public DataTemplate LinkItemTemplate { get; set; }
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item == null || !(item is CommandVm))
                return base.SelectTemplate(item, container);

            var vm = item as CommandVm;
            if (vm.IsCheckBox)
                return BarCheckItemTemplate;

            if (vm.IsLink)
                return LinkItemTemplate;

            return vm.Commands == null ? BarButtonItemTemplate : BarSubItemTemplate;
        }
    }

    public class LinkStatusToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || !(value is ELinkStatus))
                return null;

            var status = (ELinkStatus)value;
            switch (status)
            {
                case ELinkStatus.None:
                    break;
                case ELinkStatus.One:
                    return Brushes.AliceBlue;                    
                case ELinkStatus.Two:
                    return Brushes.Aquamarine;
                case ELinkStatus.Three:
                    return Brushes.Beige;
                case ELinkStatus.Four:
                    return Brushes.Blue;
                case ELinkStatus.Five:
                    return Brushes.BurlyWood;
                case ELinkStatus.Six:
                    return Brushes.Chocolate;
                case ELinkStatus.Seven:
                    return Brushes.Cyan;
                default:
                    break;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ELinkStatusToColorTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || !(value is ELinkStatus))
                return null;

            var status = (ELinkStatus)value;
            switch (status)
            {
                case ELinkStatus.None:
                    break;
                case ELinkStatus.One:
                    return "绿色";
                case ELinkStatus.Two:
                    return "橘绿";
                case ELinkStatus.Three:
                    return "品红";
                case ELinkStatus.Four:
                    return "蓝色";
                case ELinkStatus.Five:
                    return "褐色";
                case ELinkStatus.Six:
                    return "灰色";
                case ELinkStatus.Seven:
                    return "红色";
                default:
                    break;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    

}
