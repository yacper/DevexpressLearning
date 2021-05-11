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
using System.Collections.ObjectModel;

namespace DataGrid_AssignComboboxToColumn
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		public MainWindow()
		{
			InitializeComponent();

			this.DataContext = new MyViewModel();
		}
	}
	public class MyViewModel
	{
		public MyViewModel()
		{
			CreateList();
		}

		public ObservableCollection<Person> PersonList { get; set; }
		public ObservableCollection<ProductType> TypeList { get; set; }
		void CreateList()
		{
			PersonList = new ObservableCollection<Person>();
			for (int i = 0; i < 20; i++)
			{
				Person p = new Person(i);
				PersonList.Add(p);
			}
			TypeList = new ObservableCollection<ProductType>();
			for (int i = 0; i < 3; i++)
			{
				TypeList.Add(new ProductType(i));
			}
		}
	}
	public class Person
	{
		public Person(int i)
		{
			ProductName = "FirstName" + i;

			UnitPrice = i;
			Type = 0;
		}

		public string ProductName { get; set; }

		public int Type { get; set; }

		public int UnitPrice { get; set; }
	}

	public class ProductType
	{
		public ProductType(int i)
		{
			Id = i;
			TypeName = "Type" + i;
		}
		public int Id { get; set; }
		public string TypeName { get; set; }
	}
}
