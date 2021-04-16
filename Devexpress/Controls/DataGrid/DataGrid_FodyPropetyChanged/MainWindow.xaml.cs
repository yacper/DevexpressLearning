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
using System.Windows.Threading;
using DevExpress.Xpf.Core;
using ViewModels;

namespace DataGrid_FodyPropetyChanged
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : ThemedWindow
	{
		public MainWindow()
		{
			InitializeComponent();



			DispatcherTimer timer = new DispatcherTimer();

			timer.Interval =  TimeSpan.FromSeconds(0.2);
			timer.Tick     +=(s, ee)=>
			                 {
				                 foreach (Customer c in (DataContext as ViewModel).Customers)
				                 {
					                 c.Salary += 1;
					                 c.Visits += 1;
				                 }

			                 };
			timer.Start();
		}
	}
}
