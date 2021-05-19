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

namespace _2DCharts.XYDiagram2D.Stocks
{
	/// <summary>
	/// CandleStickSeries2D_Window.xaml 的交互逻辑
	/// </summary>
	public partial class CandleStickSeries2D_Window : Window
	{
		public CandleStickSeries2D_Window()
		{
			InitializeComponent();
		}
		public class ChartViewModel
		{
			public ObservableCollection<DataPoint> Data { get; private set; }
			public ChartViewModel()
			{
				Data = new ObservableCollection<DataPoint> {
				new DataPoint(new DateTime(2019,1,1), 25, 28, 24, 27),
				new DataPoint(new DateTime(2019,1,2), 27, 35, 26, 32),
				new DataPoint(new DateTime(2019,1,3), 32, 35, 27, 29),
				new DataPoint(new DateTime(2019,1,4), 29, 37, 29, 36),
				new DataPoint(new DateTime(2019,1,5), 36, 37, 32, 33),
				new DataPoint(new DateTime(2019,1,6), 36, 37, 33, 35),

				new DataPoint(new DateTime(2019,1,8), 31, 37, 30, 33),
				new DataPoint(new DateTime(2019,1,9), 32, 38, 29, 37),
				new DataPoint(new DateTime(2019,1,10), 34, 35, 32, 33),
			};
			}
		}
		public class DataPoint
		{
			public DateTime Date { get; set; }
			public double Open { get; set; }
			public double High { get; set; }
			public double Low { get; set; }
			public double Close { get; set; }
			public DataPoint(DateTime arg, double open, double high, double low, double close)
			{
				this.Date = arg;
				this.Open = open;
				this.High = high;
				this.Low = low;
				this.Close = close;
			}
		}

	}


}
