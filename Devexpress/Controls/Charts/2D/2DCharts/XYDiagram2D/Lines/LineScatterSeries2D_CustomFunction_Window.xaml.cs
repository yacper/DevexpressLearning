using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DevExpress.Xpf.Charts;

namespace _2DCharts.XYDiagram2D
{
	/// <summary>
	/// LineScatterSeries2D_CustomFunction_Window.xaml 的交互逻辑
	/// </summary>
	public partial class LineScatterSeries2D_CustomFunction_Window : Window
	{
		public LineScatterSeries2D_CustomFunction_Window()
		{
			InitializeComponent();
			CreateArchimedianSpiralPoints();
		}

		void CreateArchimedianSpiralPoints()
		{
			for (int i = 0; i < 720; i += 15)
			{
				double t = (double)i / 180 * Math.PI;
				double x = t * Math.Cos(t);
				double y = t * Math.Sin(t);
				ArchimedianSpiral.Points.Add(new SeriesPoint(x, y));
			}
		}

	}
}
