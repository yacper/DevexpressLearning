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

namespace _2DCharts.XYDiagram2D.Bars
{
	/// <summary>
	/// BarSideBySideSeries2D_Window.xaml 的交互逻辑
	/// </summary>
	public partial class BarSideBySideSeries2D_Window : Window
	{
		public BarSideBySideSeries2D_Window() { InitializeComponent(); }
	}


public class ChartViewModel
	{
		public ObservableCollection<DataSeries> Data { get; private set; }

		public ChartViewModel()
		{
			Data = new ObservableCollection<DataSeries> {
				new DataSeries{
					Name = "DevAV North",
					Values = new ObservableCollection<DataPoint> {
						new DataPoint (new DateTime(2013,12,31), 362.5),
						new DataPoint (new DateTime(2014,12,31), 348.4),
						new DataPoint (new DateTime(2015,12,31), 279.0),
						new DataPoint (new DateTime(2016,12,31), 230.9),
						new DataPoint (new DateTime(2017,12,31), 203.5),
						new DataPoint (new DateTime(2018,12,31), 197.1)
					}
				},
				new DataSeries{
					Name = "DevAV South",
					Values = new ObservableCollection<DataPoint> {
						new DataPoint (new DateTime(2013,12,31), 277.0),
						new DataPoint (new DateTime(2014,12,31), 328.5),
						new DataPoint (new DateTime(2015,12,31), 297.0),
						new DataPoint (new DateTime(2016,12,31), 255.3),
						new DataPoint (new DateTime(2017,12,31), 173.5),
						new DataPoint (new DateTime(2018,12,31), 131.8)
					}
				}
			};
		}
		public class DataSeries
		{
			public string Name { get; set; }
			public ObservableCollection<DataPoint> Values { get; set; }
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
}