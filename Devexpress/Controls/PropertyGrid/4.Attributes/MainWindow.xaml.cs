using System;
using System.Collections.Generic;
using System.ComponentModel;
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


		}


	}


	public class ViewModel
	{
		public Customer DemoCustomer { get; set; }
		public ViewModel()
		{
			DemoCustomer = new Customer()
			{
				ID = 0,
				Name = "Anita Benson",
				Email = "Anita_Benson@example.com",
				Phone = "7138638137",
				Products = new ProductList {
				new Book
				{
					ID = 47583,
					Author = "Arthur Cane",
					Title = "Digging deeper",
					Price = 14.79
				}
			}
			};
		}
	}

public interface ICustomer
{
		 string Email { get; set; }
	
}

public class CustomerBase : ICustomer
{
    [Category("Customer Info"), Description("Customer's full name")]
    public string Name { get; set; }

    [Category("Contact"), Description("Customer's email address")]
    public string Email { get; set; }
}


public class Customer:CustomerBase
	{
		[Browsable(false), ReadOnly(true)]
		public int ID { get; set; }
        [Category("Contact"), Description("Customer's phone number")]
		public string Phone { get; set; }
		[Category("Order Info"), Description("Ordered items"), DisplayName("Ordered Items")]
		public ProductList Products { get; set; }
	}

	public class Book
	{
		[DisplayName("ISBN")]
		public int ID { get; set; }
		[DefaultValue("hha")]
		public string Author { get; set; }
		public string Title { get; set; }
		public double Price { get; set; }
	}

	public class ProductList : List<Object>
	{
		public ProductList() : base() { }
		public override string ToString()
		{
			return "Product List";
		}
	}


}
