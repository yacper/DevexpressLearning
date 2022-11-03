using DevExpress.Xpf.Core;
using DevExpress.Xpf.Grid;
using Neo.Api.Attributes;
using NeoControls;
using NeoTrader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace NeoTrader.UI.Controls
{    
    public class RDataGrid : GridControl
    {
        //public static DependencyProperty ToolsBgBrushProperty = DependencyProperty.Register("ToolsBgBrush", typeof(Brush), typeof(RDataGrid), new PropertyMetadata(Brushes.Blue));
        //public Brush ToolsBgBrush
        //{
        //    get => (Brush)GetValue(ToolsBgBrushProperty);
        //    set => SetValue(ToolsBgBrushProperty, value);
        //}

        public static DependencyProperty AlwaysShowToolBarProperty = DependencyProperty.Register(nameof(AlwaysShowToolBar), typeof(bool), typeof(RDataGrid), new PropertyMetadata(false));
        public bool AlwaysShowToolBar
        {
            get => (bool)GetValue(AlwaysShowToolBarProperty);
            set => SetValue(AlwaysShowToolBarProperty, value);
        }

        // Tools 数据
        //public static DependencyProperty RowToolsProperty = DependencyProperty.Register("RowTools", typeof(IEnumerable<CommandVm>), typeof(RGridControl), 
        //    new PropertyMetadata(null, (d,e) => 
        //    {
        //        //var rc = d as RGridControl;
        //        //rc!.RowTools = (IEnumerable<IRowTools>)e.NewValue;
        //    }));

        //public IEnumerable<CommandVm> RowTools 
        //{
        //    get => (IEnumerable<CommandVm>)GetValue(RowToolsProperty);
        //    set => SetValue(RowToolsProperty, value);            
        //}

        public static DependencyProperty ToolCommandsTemplateProperty = DependencyProperty.Register(nameof(ToolCommandsTemplate), typeof(ObservableCollection<CommandVm>), typeof(RDataGrid));

        public ObservableCollection<CommandVm> ToolCommandsTemplate 
        { 
            get => (ObservableCollection<CommandVm>)GetValue(ToolCommandsTemplateProperty); 
            set => SetValue(ToolCommandsTemplateProperty, value);
        }

        public static DependencyProperty CellTemplateSelectorProperty = DependencyProperty.Register(nameof(CellTemplateSelector), typeof(DataTemplateSelector), typeof(RDataGrid),
            new PropertyMetadata(null, (d, e) => 
            {

            }));

        public DataTemplateSelector CellTemplateSelector
        {
            get=>(DataTemplateSelector)GetValue(CellTemplateSelectorProperty);
            set=>SetValue(CellTemplateSelectorProperty, value);
        }

        public RDataGrid(): base()
        {
            SelectionMode = MultiSelectMode.Row;
            ToolCommandsTemplate  = new();
            if (View == null)
                View = CreateTableView();

            Loaded += RGridControl_Loaded;            
        }

        private void RGridControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (ItemsSource == null)
                return;

            if(Columns.Count() == 0 && ColumnsSource == null)            
                ColumnsSource = CreateColumns();                
            
            if(ColumnGeneratorTemplate == null && ColumnsSource != null)
                ColumnGeneratorTemplate = (DataTemplate)App.Current.FindResource("DefaultColumnTemplate");          // 指定默认Column模板
        }

        private TableView CreateTableView()
        {
            var tv = new TableView();            
            tv.ShowIndicator = false;
            tv.ShowGroupPanel = false;
            tv.AllowDragDrop = true;

            tv.HighlightItemOnHover = true;
            tv.ColumnSortClearMode = ColumnSortClearMode.Click;
            
            tv.RowStyle = new Style(typeof(RowControl));
            tv.RowStyle.Setters.Add(new Setter() 
            {
                Property = RowControl.TemplateProperty,
                Value = App.Current.FindResource("RDataGrid_RowControlTemplate")
            });

            return tv;
        }

        private IEnumerable<RColumnItemData> CreateColumns()
        {
            List<RColumnItemData> columns = new List<RColumnItemData>();
            Type type = (ItemsSource as IList)[0].GetType();
            foreach (var p in type.GetProperties().Where(_=> { return _.GetCustomAttribute(typeof(StatAttribute)) != null; }))
            {
                RColumnItemData rcid = new RColumnItemData();
                rcid.FieldName = p.Name;
                columns.Add(rcid);
            }

            return columns;
        }
    }
}
