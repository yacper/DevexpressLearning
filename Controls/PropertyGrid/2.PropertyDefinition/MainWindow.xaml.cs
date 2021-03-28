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


			DataContext = new Customer()
			{
				ID = 1,
				FirstName = "Nancy",
				LastName = "Davolio",
				Gender = Gender.Female,
				BirthDate = new DateTime(1948, 8, 12),
				Phone = "7138638137"
			};

		}





	}


	public class Customer
	{
		public int ID { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public Gender Gender { get; set; }
		public DateTime BirthDate { get; set; }
		public string Phone { get; set; }
	}
	public enum Gender { Male, Female }

}
