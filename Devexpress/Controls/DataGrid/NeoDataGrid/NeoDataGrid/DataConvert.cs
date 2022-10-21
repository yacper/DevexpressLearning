using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Grid.TreeList;
using NeoDataGrid.Control;
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

    public class RowControlDataContentConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length != 2 || !(values[0] is RowData || values[0] is TreeListRowData) || !(values[1] is RGridControl))
                return null;

            var rgc = values[1] as RGridControl;
            if (values[0] is RowData)
            {
                if (rgc!.RowTools == null || rgc!.RowTools.Count() == 0)
                    return rgc!.DefaultTools;

                return rgc!.RowTools.First();
            }

            int level = (values[0] as TreeListRowData)!.Node.ActualLevel;
            if(rgc!.RowTools == null || rgc!.RowTools.Count() == 0)
                return rgc!.DefaultTools;

            var tools = rgc.RowTools.Where(_ => { return _.Level == level; }).FirstOrDefault();
            return tools == null ? rgc.DefaultTools : tools;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class RowContorMoreVisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value == null || !(value is RowToolsViewMode))
                return Visibility.Collapsed;

            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class RowContrtolMoreContextDataConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || !(value is RowTools))
                return null;

            RowTools tools = (RowTools)value;
            var unuse = tools.ToolVms.Where(_ => { return !_.IsNormalUse; }).Count();
            if (unuse > 0 && tools.MoreVm == null)
                tools.CreateMoreVM();

            return tools.MoreVm;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class RowControlNormalUseListConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value == null || !(value is IEnumerable<RowToolsViewMode>))
                return null;

            return (value as IEnumerable<RowToolsViewMode>)!.Where(_ => { return _.IsNormalUse; });
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class RowControlContextMenuConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || !(value is IEnumerable<RowToolsViewMode>))
                return null;

            return RControlUtils.CreateRowControlContextMenu((IEnumerable<RowToolsViewMode>)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
