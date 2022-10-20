using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace NeoTrader
{
    public class DisplayModeToVisibleConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length != 2 || !(values[0] is DisplayMode) || !(values[1] is DisplayMode))
                return Visibility.Collapsed;

            return (DisplayMode)values[0] == (DisplayMode)values[1] ? Visibility.Visible : Visibility.Collapsed;

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ButtonItemTemplaSelector : DataTemplateSelector
    {
        public DataTemplate NormalBtnTemplate { get; set; }
        public DataTemplate IconTextTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (!(item is RowToolsViewMode))
                return base.SelectTemplate(item, container);

            var mode = item as RowToolsViewMode;
            if (mode == null)
                return base.SelectTemplate(item, container);

            if (mode.IsGlyphText)
                return IconTextTemplate;

            return NormalBtnTemplate;
        }
    }

    public class TableRowToolsVisibleConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length != 2 || !(values[0] is bool) || !(values[1] is bool))
                return Visibility.Collapsed;

            bool IsFixed = (bool)values[0];
            bool IsMuseMove = (bool)values[1];
            if (IsFixed)
                return Visibility.Visible;

            return IsMuseMove ? Visibility.Visible : Visibility.Collapsed;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
