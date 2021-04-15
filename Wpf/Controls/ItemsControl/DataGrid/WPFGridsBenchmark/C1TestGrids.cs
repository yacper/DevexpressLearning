using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Grids = new Dictionary<string, Type>();
            GridsForAutoSizeTesting = new List<Type>();
            Grids.Add("Microsoft DataGrid", typeof(MSDataGrid));
            GridsForAutoSizeTesting.Add(typeof(MSDataGrid));
            Grids.Add("ComponentOne C1FlexGrid", typeof(C1FlexGrid));
            Grids.Add("ComponentOne C1DataGrid", typeof(C1DataGrid));
            GridsForAutoSizeTesting.Add(typeof(C1DataGrid));
        }

        protected void SetGrid(Control grid)
        {
            this.Content = grid;
        }
        public abstract void Load(IList data);
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
            _grid.ScrollIntoView(item);
        }
    }

    public class C1FlexGrid : TestGrid
    {
        C1.WPF.FlexGrid.C1FlexGrid _grid = null;
        public C1FlexGrid(bool fixedColumnWidth)
        {
            _grid = new C1.WPF.FlexGrid.C1FlexGrid();
            // uses some predefined column width depending on underlying data
            // autowidth is only possible with method call
            _grid.AllowAddNew = true;
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
            _grid.ScrollIntoView(rowNumber, 0);
        }
    }

    public class C1DataGrid : TestGrid
    {
        C1.WPF.DataGrid.C1DataGrid _grid = null;
        public C1DataGrid(bool fixedColumnWidth)
        {
            _grid = new C1.WPF.DataGrid.C1DataGrid();
            // default column sizing is AutoStar, always shows full content, might add some space depending on screen size and default column sizing
            if (fixedColumnWidth)
            {
                _grid.ColumnWidth = new C1.WPF.DataGrid.DataGridLength(150);
            }
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
            _grid.ScrollIntoView(rowNumber, 0);
        }
    }
}
