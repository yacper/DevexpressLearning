using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace _2DCharts.XYDiagram2D
{
	/// <summary>
	/// SplineSeries2D_Window.xaml 的交互逻辑
	/// </summary>
	public partial class SplineSeries2D_Window : Window
	{
		public SplineSeries2D_Window()
		{
			InitializeComponent();
		}
	}

	public class ChartViewModel
	{
		public ObservableCollection<DataPoint> Data { get; private set; }
		public ChartViewModel()
		{
			int lastYear = DateTime.Now.Year - 1;
			Data = new ObservableCollection<DataPoint> {
						new DataPoint (new DateTime(lastYear,1,1), 138.7),
						new DataPoint (new DateTime(lastYear,2,1), 141.4),
						new DataPoint (new DateTime(lastYear,3,1), 159.5),
						new DataPoint (new DateTime(lastYear,4,1), 160.7),
						new DataPoint (new DateTime(lastYear,5,1), 148.8),
						new DataPoint (new DateTime(lastYear,6,1), 166.6)
				};
		}
	}
	public class DataPoint
	{
		public DateTime Argument { get; set; }
		public double Value { get; set; }
		public DataPoint(DateTime argument, double value)
		{
			Argument = argument;
			Value = value;
		}
	}
}
