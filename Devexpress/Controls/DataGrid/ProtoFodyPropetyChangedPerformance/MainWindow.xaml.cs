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
using DataGrid_ProtoFodyPropetyChanged.ViewModels;
using DevExpress.Dialogs.Core.View;

namespace ProtoFodyPropetyChangedPerformance
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

		private void Gen_Click(object sender, RoutedEventArgs e)
		{
			(DataContext as MainViewModel).Generate(Convert.ToInt32(Num.Text));
		}

		DispatcherTimer timer;
		private void Start_Click(object sender, RoutedEventArgs e)
		{
			double updatetime = Convert.ToDouble(Interval.Text);

			Stop_Click(null, null);


			timer = new DispatcherTimer();

			timer.Interval = TimeSpan.FromSeconds(updatetime);
			timer.Tick += (s, ee) =>
						  {
							  (DataContext as MainViewModel).Change();
						  };
			timer.Start();
		}

		private void Stop_Click(object sender, RoutedEventArgs e)
		{
			if (timer != null)
			{
				timer.Stop();
				timer = null;
			}
		}
	}
}
