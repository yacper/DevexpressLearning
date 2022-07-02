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
using Plotly.NET;
using Plotly.NET.ImageExport;
using GenericChartExtensions = Plotly.NET.CSharp.GenericChartExtensions;

namespace Plotly_Test
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow:Window
	{
		public MainWindow()
		{
			InitializeComponent();

			double[]                  x     = new double[] { 1, 2 };
			double[]                  y     = new double[] { 5, 10 };
			GenericChart.GenericChart chart = Chart2D.Chart.Point<double, double, string>(x: x, y: y);
			chart.SavePNG("a");
			chart.ShowAsImage(StyleParam.ImageFormat.PNG);
			chart.Show();
		}
	}
}
