using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using DataGrid_ProtoFodyPropetyChanged.ViewModels;

namespace DataGrid_ProtoFodyPropetyChanged.Views
{
	/// <summary>
	/// Interaction logic for View1.xaml
	/// </summary>
	public partial class MainView : UserControl
	{
		public MainView()
		{
			InitializeComponent();


			DispatcherTimer timer = new DispatcherTimer();

			timer.Interval = TimeSpan.FromSeconds(0.2);
			timer.Tick += (s, ee) =>
						  {
							  foreach (Customer c in (DataContext as MainViewModel).Customers)
							  {
								  c.Salary += 1;
								  c.Visits += 1;
							  }

						  };
			timer.Start();
		}
	}
}
