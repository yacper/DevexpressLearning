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
using DevExpress.Mvvm.Native;
using pgrid_collection;

namespace DXPropertyGrid
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		public MainWindow()
		{
			InitializeComponent();
			grid.ItemsSource = new ProductList();
		}

		private void pGrid_CellValueChanged(object sender, DevExpress.Xpf.PropertyGrid.CellValueChangedEventArgs args)
		{
			var rowhandle = grid.GetSelectedRowHandles()[0];
			grid.RefreshRow(rowhandle);
		}
	}
	public class ItemInitializer : IInstanceInitializer
	{
		public ItemInitializer()
		{
		}

		object IInstanceInitializer.CreateInstance(TypeInfo type)
		{
			var item = new Supplier();
			item.FirstName = "FIRSTNAME";
			item.LastName = "LASTNAME";
			item.Phone = "PHONE";
			return item;
		}


		IEnumerable<TypeInfo> IInstanceInitializer.Types
		{
			get
			{
				return new List<TypeInfo>() {
						new TypeInfo(typeof(Supplier), "New Supplier"),
					};
			}
		}
	}
}
