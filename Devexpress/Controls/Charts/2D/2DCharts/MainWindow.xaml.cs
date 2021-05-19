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
using _2DCharts.XYDiagram2D;

namespace _2DCharts
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

		private void PointSeries2D_Click(object sender, RoutedEventArgs e)
		{
			PointSeries2D_Window win = new PointSeries2D_Window();
			win.Show();



		}

		private void LineScatterSeries2D_CustomFunction_Click(object sender, RoutedEventArgs e)
		{
			LineScatterSeries2D_CustomFunction_Window win = new LineScatterSeries2D_CustomFunction_Window();
			win.Show();
		}

		

	}
}
