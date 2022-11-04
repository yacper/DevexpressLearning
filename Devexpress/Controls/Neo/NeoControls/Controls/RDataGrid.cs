using DevExpress.Mvvm;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Grid.TreeList;
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
using System.Windows.Input;
using System.Windows.Media;

namespace NeoTrader.UI.Controls
{    
    public class RDataGrid : GridControl
    {
        public static DependencyProperty AlwaysShowToolBarProperty = DependencyProperty.Register(nameof(AlwaysShowToolBar), typeof(bool), typeof(RDataGrid), new PropertyMetadata(false));
        public bool AlwaysShowToolBar
        {
            get => (bool)GetValue(AlwaysShowToolBarProperty);
            set => SetValue(AlwaysShowToolBarProperty, value);
        }

        public static DependencyProperty ToolCommandsTemplateProperty = DependencyProperty.Register(nameof(ToolCommandsTemplate), typeof(ObservableCollection<CommandVm>), typeof(RDataGrid));

        public ObservableCollection<CommandVm> ToolCommandsTemplate 
        { 
            get => (ObservableCollection<CommandVm>)GetValue(ToolCommandsTemplateProperty); 
            set => SetValue(ToolCommandsTemplateProperty, value);
        }

        public static DependencyProperty ChildToolCommandsTemplateProperty = DependencyProperty.Register(nameof(ChildToolCommandsTemplate), typeof(ObservableCollection<CommandVm>), typeof(RDataGrid));
        public ObservableCollection<CommandVm> ChildToolCommandsTemplate
        {
            get => (ObservableCollection<CommandVm>)GetValue(ChildToolCommandsTemplateProperty);
            set => SetValue(ChildToolCommandsTemplateProperty, value);
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

        public ICommand RowControlLoadCommand { get; private set; }
        public ICommand RMouseDoubleClickCommand { get; private set; }
        public ICommand CellsControlParentPannelLoadCommand { get; private set; }

        public RDataGrid(): base()
        {
            SelectionMode = MultiSelectMode.Row;
            ToolCommandsTemplate  = new();
            if (View == null)
                View = CreateTableView();

            InitCommand();
            Loaded += RGridControl_Loaded;
        }

        private void InitCommand()
        {
            InitRowControlLoadCommand();
            InitRMouseDoubleClickCommand();
            InitCellsControlParentPannelLoadCommand();
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
                RColumnItemData rcid = new RColumnItemData(p.Name);                
                columns.Add(rcid);
            }

            return columns;
        }

        private void InitCellsControlParentPannelLoadCommand()
        {
            CellsControlParentPannelLoadCommand = new DelegateCommand<Grid>((g) =>
            {
                return;
                var rd = UiUtils.UIUtils.GetParentObject<RDataGrid>(g);
                if (rd.View is TableView)
                    return;

                DXImage dXImage = new DXImage();
                dXImage.Name = "PART_ExpandedTips";
                dXImage.Source = Images.SortDsec;
                dXImage.Width = 10;
                dXImage.Height = 10;
                dXImage.Margin = new Thickness(10, 0, 0, 0);

                var border = UiUtils.UIUtils.GetChildObject<RowFitBorder>(g, typeof(RowFitBorder));

                int c = Grid.GetColumn(border);
                g.ColumnDefinitions.Insert(c, new System.Windows.Controls.ColumnDefinition() { Width = new GridLength(30, GridUnitType.Pixel) });

                Grid.SetColumn(dXImage, c);
                g.Children.Add(dXImage);
            });
        }

        private void InitRowControlLoadCommand()
        {
            RowControlLoadCommand = new DelegateCommand<RowControl>(rc =>
            {
                RowData rd = rc.DataContext as RowData;
                var rdg = UiUtils.UIUtils.GetParentObject<RDataGrid>(rc);
                var toolBarControl = UiUtils.UIUtils.GetChildObject<ToolBarControl>(rc, typeof(ToolBarControl));

                if (!(rd is TreeListRowData) || (rd as TreeListRowData).Node.HasChildren || rdg.ChildToolCommandsTemplate == null)
                {
                    if (rdg.ToolCommandsTemplate == null)
                        return;
                    toolBarControl.ItemsSource = rdg.ToolCommandsTemplate.Select(x => x.Clone(rd.Row));
                }
                else                
                    toolBarControl.ItemsSource = rdg.ChildToolCommandsTemplate.Select(x => x.Clone(rd.Row));
                
                rd.ContentChanged += (s, e) =>
                {
                    if (!(rd is TreeListRowData) || (rd as TreeListRowData).Node.HasChildren || rdg.ChildToolCommandsTemplate == null)
                    {
                        if (rdg.ToolCommandsTemplate == null)
                            return;
                        toolBarControl.ItemsSource = rdg.ToolCommandsTemplate.Select(x => x.Clone(rd.Row));
                    }
                    else
                        toolBarControl.ItemsSource = rdg.ChildToolCommandsTemplate.Select(x => x.Clone(rd.Row));
                };
            });
        }

        private void InitRMouseDoubleClickCommand()
        {
            RMouseDoubleClickCommand = new DelegateCommand<RowControl>((rc) =>
            {
                if (!(rc.DataContext is TreeListRowData))
                    return;

                var rowData = rc.DataContext as TreeListRowData;
                rowData.Node.IsExpanded = !rowData.IsExpanded;
            });
        }
    }
}
