using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace WPFGridsBenchmark
{
    public abstract class TestGrid : UserControl
    {
        public static Dictionary<string, Type> Grids;
        public static List<Type> GridsForAutoSizeTesting;
        static TestGrid()
        {
//            Xceed.Wpf.DataGrid.Licenser.LicenseKey = "DGP5927KNK7BAEK6BBA";

            Grids = new Dictionary<string, Type>();
            GridsForAutoSizeTesting = new List<Type>();
            Grids.Add("Microsoft DataGrid", typeof(MSDataGrid));
            GridsForAutoSizeTesting.Add(typeof(MSDataGrid));

            //Grids.Add("ComponentOne C1FlexGrid", typeof(C1FlexGrid));
            //Grids.Add("ComponentOne C1DataGrid", typeof(C1DataGrid));
            //GridsForAutoSizeTesting.Add(typeof(C1DataGrid));
            //Grids.Add("Infragistics DataGrid", typeof(IGDataGrid));
            //GridsForAutoSizeTesting.Add(typeof(IGDataGrid));
            Grids.Add("DevExpress GridControl", typeof(DXGridControl));
            //Grids.Add("SyncFusion SfDataGrid", typeof(SFDataGrid));
            //Grids.Add("Telerik RadGridView", typeof(TelerikRadGridView));
            //GridsForAutoSizeTesting.Add(typeof(TelerikRadGridView));
            //Grids.Add("Xceed DataGrid", typeof(XceedDataGrid));
        }

//		public  object Grid { get { return Content; } }

        protected void SetGrid(Control grid)
        {
            this.Content = grid;
        }
        public virtual void Load(MainWindow window, IList data, BindingMode mode = BindingMode.OneWay, bool isasync = true)
		{

		}
        public virtual void Dispose()
        {
            this.Content = null;
        }
        public abstract void Sort(bool ascending);
        public virtual string GetGridInfo()
        {
            if (this.Content != null)
            {
                System.Reflection.AssemblyName asName = this.Content.GetType().Assembly.GetName();
                return asName.Name + "." + asName.Version;
            }
            return String.Empty;
        }
        public abstract void ScrollInView(int rowNumber);
    }

    public class MSDataGrid : TestGrid
    {
        DataGrid _grid = null;

		public new DataGrid Grid { get { return this.Content as DataGrid; } }

        public MSDataGrid(bool fixedColumnWidth)
        {
            _grid = new DataGrid();
            // default column sizing is SizeToHeader, finally sets column width by content
            if (fixedColumnWidth)
            {
                _grid.ColumnWidth = new DataGridLength(150);
            }
            SetGrid(_grid);
        }

        public override void Dispose()
        {
            _grid.ItemsSource = null;
            base.Dispose();
        }

        public override void Load(MainWindow window, IList data, BindingMode mode = BindingMode.OneWay, bool isasync = true)
        {
			base.Load(window, data, mode, isasync);
            if (data == null)
            {
                _grid.ItemsSource = null;
            }
            else
            {
	            //_grid.SetBinding(ItemsControl.ItemsSourceProperty,
	            //                 new Binding("Datas") {Source = window, Mode = mode, IsAsync = isasync});

	            _grid.SetBinding(ItemsControl.ItemsSourceProperty,
	                             new Binding("Datas") {Source = window, Mode = BindingMode.TwoWay, IsAsync = false});
	            //_grid.ItemsSource = new ListCollectionView(data);
            }
        }

        public override void Sort(bool ascending)
        {
            ICollectionView dataView = CollectionViewSource.GetDefaultView(_grid.ItemsSource);
            dataView.SortDescriptions.Clear();
            dataView.SortDescriptions.Add(new SortDescription("ID", ascending ? ListSortDirection.Ascending : ListSortDirection.Descending));
        }

        public override void ScrollInView(int rowNumber)
        {
            object item = ((ListCollectionView)_grid.ItemsSource).GetItemAt(rowNumber);
            _grid.ScrollIntoView(item);
        }
    }

	//    public class C1FlexGrid : TestGrid
	//    {
	//        C1.WPF.FlexGrid.C1FlexGrid _grid = null;
	//        public C1FlexGrid(bool fixedColumnWidth)
	//        {
	//            _grid = new C1.WPF.FlexGrid.C1FlexGrid();
	//            // uses some predefined column width depending on underlying data
	//            // autowidth is only possible by method calls
	//            _grid.AllowAddNew = true;
	//            SetGrid(_grid);
	//        }

	//        public override void Dispose()
	//        {
	//            _grid.ItemsSource = null;
	//            base.Dispose();
	//        }

	//        public override void Load(IList data)
	//        {
	//            if (data == null)
	//            {
	//                _grid.ItemsSource = null;
	//            }
	//            else
	//            {
	//                _grid.ItemsSource = new ListCollectionView(data);
	//            }
	//        }

	//        public override void Sort(bool ascending)
	//        {
	//            ICollectionView dataView = CollectionViewSource.GetDefaultView(_grid.ItemsSource);
	//            dataView.SortDescriptions.Clear();
	//            dataView.SortDescriptions.Add(new SortDescription("ID", ascending ? ListSortDirection.Ascending : ListSortDirection.Descending));
	//        }
	//        public override void ScrollInView(int rowNumber)
	//        {
	//            _grid.ScrollIntoView(rowNumber, 0);
	//        }
	//    }

	//    public class C1DataGrid : TestGrid
	//    {
	//        C1.WPF.DataGrid.C1DataGrid _grid = null;
	//        public C1DataGrid(bool fixedColumnWidth)
	//        {
	//            _grid = new C1.WPF.DataGrid.C1DataGrid();
	//            // default column sizing is AutoStar, always shows full content, might add some space depending on screen size and default column sizing
	//            if (fixedColumnWidth)
	//            {
	//                _grid.ColumnWidth = new C1.WPF.DataGrid.DataGridLength(150);
	//            }
	//            SetGrid(_grid);
	//        }
	//        public override void Dispose()
	//        {
	//            _grid.ItemsSource = null;
	//            base.Dispose();
	//        }

	//        public override void Load(IList data)
	//        {
	//            if (data == null)
	//            {
	//                _grid.ItemsSource = null;
	//            }
	//            else
	//            {
	//                _grid.ItemsSource = new ListCollectionView(data);
	//            }
	//        }

	//        public override void Sort(bool ascending)
	//        {
	//            ICollectionView dataView = CollectionViewSource.GetDefaultView(_grid.ItemsSource);
	//            dataView.SortDescriptions.Clear();
	//            dataView.SortDescriptions.Add(new SortDescription("ID", ascending ? ListSortDirection.Ascending : ListSortDirection.Descending));
	//        }
	//        public override void ScrollInView(int rowNumber)
	//        {
	//            _grid.ScrollIntoView(rowNumber, 0);
	//        }
	//    }

	//    public class IGDataGrid : TestGrid
	//    {
	//        Infragistics.Windows.DataPresenter.XamDataGrid _grid = null;
	//        public IGDataGrid(bool fixedColumnWidth)
	//        {
	//            _grid = new Infragistics.Windows.DataPresenter.XamDataGrid();
	//            // default column sizing uses fixed column width. Don't see good way to change it
	//            _grid.GroupByAreaLocation = Infragistics.Windows.DataPresenter.GroupByAreaLocation.None;
	//            _grid.FieldLayoutSettings.SortEvaluationMode = Infragistics.Windows.DataPresenter.SortEvaluationMode.UseCollectionView;
	//            _grid.FieldLayoutSettings.AllowAddNew = true;
	//            // by default columns have fixed size
	//            if (!fixedColumnWidth)
	//            {
	//                _grid.FieldSettings.Width = Infragistics.Windows.DataPresenter.FieldLength.InitialAuto;
	//                _grid.FieldSettings.AutoSizeScope = Infragistics.Windows.DataPresenter.FieldAutoSizeScope.RecordsInView;
	//                _grid.FieldSettings.AutoSizeOptions = Infragistics.Windows.DataPresenter.FieldAutoSizeOptions.All;
	//            }
	//            _grid.FieldLayoutSettings.AddNewRecordLocation = Infragistics.Windows.DataPresenter.AddNewRecordLocation.OnBottom;
	//            SetGrid(_grid);
	//        }

	//        public override void Dispose()
	//        {
	//            _grid.DataSource = null;
	//            base.Dispose();
	//        }

	//        public override void Load(IList data)
	//        {
	//            if (data == null)
	//            {
	//                _grid.DataSource = null;
	//            }
	//            else
	//            {
	//                _grid.DataSource = new ListCollectionView(data);
	//            }
	//        }

	//        public override void Sort(bool ascending)
	//        {
	//            // Sorting via XamDataGrid is faster, use it.
	//            _grid.FieldLayouts[0].SortedFields.Clear();
	//            Infragistics.Windows.DataPresenter.FieldSortDescription sort = new Infragistics.Windows.DataPresenter.FieldSortDescription();
	//            sort.Direction = ascending ? ListSortDirection.Ascending : ListSortDirection.Descending;
	//            sort.FieldName = "ID";
	//            _grid.FieldLayouts[0].SortedFields.Add(sort);
	//#if (false) // It is extremely slow on big data
	//            if (_grid.FieldLayouts.Count > 0)
	//              {
	//                  // XamDataGrid saves sorting into this collection, so if you want to re-sort, you should clear all saved sortings first
	//                  _grid.FieldLayouts[0].SortedFields.Clear();
	//              }
	//              ICollectionView dataView = CollectionViewSource.GetDefaultView(_grid.DataSource);
	//              dataView.SortDescriptions.Clear();
	//              dataView.SortDescriptions.Add(new SortDescription("ID", ascending ? ListSortDirection.Ascending : ListSortDirection.Descending));
	//#endif
	//        }
	//        public override void ScrollInView(int rowNumber)
	//        {
	//            object item = ((ListCollectionView)_grid.DataSource).GetItemAt(rowNumber);
	//            _grid.BringDataItemIntoView(item, false);
	//        }
	//    }

	public class DXGridControl : TestGrid
	{
		DevExpress.Xpf.Grid.GridControl _grid = null;

		public new DevExpress.Xpf.Grid.GridControl Grid { get { return _grid as DevExpress.Xpf.Grid.GridControl; } }
		public DXGridControl(bool fixedColumnWidth)
		{
			_grid = new DevExpress.Xpf.Grid.GridControl();
			_grid.AutoGenerateColumns = DevExpress.Xpf.Grid.AutoGenerateColumnsMode.AddNew;
			var tableView = new DevExpress.Xpf.Grid.TableView();
			_grid.View = tableView;
			tableView.ShowGroupPanel = false;
			// by default columns have fixed size
			// autowidth is only possible by method calls
			tableView.NewItemRowPosition = DevExpress.Xpf.Grid.NewItemRowPosition.Bottom;
			SetGrid(_grid);
		}

		public override void Dispose()
		{
			_grid.ItemsSource = null;
			base.Dispose();
		}

		public override void Load(MainWindow window, IList data, BindingMode mode = BindingMode.OneWay, bool isasync = true)
		{
			base.Load(window, data, mode, isasync);

			if (data == null)
			{
				_grid.ItemsSource = null;
			}
			else
			{
				_grid.ItemsSource = new ListCollectionView(data);
			}
		}

		public override void Sort(bool ascending)
		{
			ICollectionView dataView = CollectionViewSource.GetDefaultView(_grid.ItemsSource);
			dataView.SortDescriptions.Clear();
			dataView.SortDescriptions.Add(new SortDescription("ID", ascending ? ListSortDirection.Ascending : ListSortDirection.Descending));
		}
		public override void ScrollInView(int rowNumber)
		{
			_grid.View.ScrollIntoView(rowNumber);
		}
	}

	//    public class SFDataGrid : TestGrid
	//    {
	//        Syncfusion.UI.Xaml.Grid.SfDataGrid _grid = null;

	//        public SFDataGrid(bool fixedColumnWidth)
	//        {
	//            _grid = new Syncfusion.UI.Xaml.Grid.SfDataGrid();
	//            _grid.AllowEditing = true;
	//            _grid.AllowGrouping = false;
	//            // Uses fixed column width by default. No autostar setting
	//            /*if (!fixedColumnWidth) // it's extremely slow on large data, exclude it from testing https://www.syncfusion.com/forums/120859/sfdatagrid-trouble-rendering
	//            {
	//                _grid.ColumnSizer = Syncfusion.UI.Xaml.Grid.GridLengthUnitType.AutoWithLastColumnFill;
	//            }*/
	//            _grid.AddNewRowPosition = Syncfusion.UI.Xaml.Grid.AddNewRowPosition.Bottom;
	//            SetGrid(_grid);
	//        }

	//        public override void Dispose()
	//        {
	//            _grid.ItemsSource = null;
	//            base.Dispose();
	//        }

	//        public override void Load(IList data)
	//        {
	//            if (data == null)
	//            {
	//                _grid.ItemsSource = null;
	//            }
	//            else
	//            {
	//                _grid.ItemsSource = new ListCollectionView(data);
	//            }
	//        }

	//        public override void Sort(bool ascending)
	//        {
	//            _grid.SortColumnDescriptions.Clear();
	//            Syncfusion.UI.Xaml.Grid.SortColumnDescription sort = new Syncfusion.UI.Xaml.Grid.SortColumnDescription();
	//            sort.ColumnName = "ID";
	//            sort.SortDirection = ascending ? ListSortDirection.Ascending : ListSortDirection.Descending;
	//            _grid.SortColumnDescriptions.Add(sort);

	//#if (false) // it doesn't work at all
	//            ICollectionView dataView = CollectionViewSource.GetDefaultView(_grid.ItemsSource);
	//            dataView.SortDescriptions.Clear();
	//            dataView.SortDescriptions.Add(new SortDescription("ID", ascending ? ListSortDirection.Ascending : ListSortDirection.Descending));
	//#endif
	//        }
	//        public override void ScrollInView(int rowNumber)
	//        {
	//            _grid.ScrollInView(new Syncfusion.UI.Xaml.ScrollAxis.RowColumnIndex() { RowIndex = rowNumber, ColumnIndex = 0 });
	//        }
	//    }

	//    public class TelerikRadGridView : TestGrid
	//    {
	//        Telerik.Windows.Controls.RadGridView _grid = null;

	//        public TelerikRadGridView(bool fixedColumnWidth)
	//        {
	//            _grid = new Telerik.Windows.Controls.RadGridView();
	//            _grid.ShowSearchPanel = false;
	//            _grid.ShowGroupPanel = false;
	//            // default column sizing is SizeToHeader, finally sizes columns to show all content
	//            if (fixedColumnWidth)
	//            {
	//                _grid.ColumnWidth = new Telerik.Windows.Controls.GridViewLength(150);
	//            }
	//            _grid.NewRowPosition = Telerik.Windows.Controls.GridView.GridViewNewRowPosition.Bottom;
	//            SetGrid(_grid);
	//        }

	//        public override void Dispose()
	//        {
	//            _grid.ItemsSource = null;
	//            base.Dispose();
	//        }

	//        public override void Load(IList data)
	//        {
	//            if (data == null)
	//            {
	//                _grid.ItemsSource = null;
	//            }
	//            else
	//            {
	//                _grid.ItemsSource = new ListCollectionView(data);
	//            }
	//        }

	//        public override void Sort(bool ascending)
	//        {
	//            ICollectionView dataView = CollectionViewSource.GetDefaultView(_grid.ItemsSource);
	//            dataView.SortDescriptions.Clear();
	//            dataView.SortDescriptions.Add(new SortDescription("ID", ascending ? ListSortDirection.Ascending : ListSortDirection.Descending));
	//        }
	//        public override void ScrollInView(int rowNumber)
	//        {
	//            _grid.ScrollIndexIntoView(rowNumber);
	//        }
	//    }

	/* // gives exception at runtime, so implement it with xaml (see XceedDataGrid.xaml)
       public class XceedDataGrid : TestGrid
       {
           Xceed.Wpf.DataGrid.DataGridControl _grid = null;

           public XceedDataGrid()
           {
               _grid = new Xceed.Wpf.DataGrid.DataGridControl();
               SetGrid(_grid);
           }

           public override void Dispose()
           {
               _grid.ItemsSource = null;
               base.Dispose();
           }

           public override void Load(IList data)
           {
                if (data == null)
                {
                    _grid.ItemsSource = null;
                }
                else
                {
                    _grid.ItemsSource = new ListCollectionView(data);
                }
           }

           public override void Sort(bool ascending)
           {
               ICollectionView dataView = CollectionViewSource.GetDefaultView(_grid.ItemsSource);
               dataView.SortDescriptions.Clear();
               dataView.SortDescriptions.Add(new SortDescription("ID", ascending ? ListSortDirection.Ascending : ListSortDirection.Descending));
           }
           public override void ScrollInView(int rowNumber)
           {
               object item = ((ListCollectionView)_grid.ItemsSource).GetItemAt(rowNumber);
               _grid.BringItemIntoView(item);
           }
       }*/
}
