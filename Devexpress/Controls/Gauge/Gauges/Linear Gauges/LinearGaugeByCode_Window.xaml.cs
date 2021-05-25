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
using DevExpress.Xpf.Gauges;

namespace Gauges.Linear_Gauges
{
	/// <summary>
	/// LinearGaugeByCode_Window.xaml 的交互逻辑
	/// </summary>
	public partial class LinearGaugeByCode_Window : Window
	{
		public LinearGaugeByCode_Window()
		{
			InitializeComponent();

			LinearGaugeControl gauge = new LinearGaugeControl();
			LinearScale scale = new LinearScale()
			{
				StartValue = 0,
				EndValue = 12,
				MajorIntervalCount = 12,
				MinorIntervalCount = 5
			};
			// mode
			gauge.Model = new LinearThemeableModel();

			// levelbar
			scale.LevelBars.Add(new LinearScaleLevelBar(){Value = 50});
		

			// layer
			//scale.Layers.Add(new ArcScaleLayer());

			// ranges
			scale.Ranges.Add(new LinearScaleRange(){EndValue  = new RangeValue(4, RangeValueType.Absolute)});

			// range bar
			//scale.RangeBars.Add(new ArcScaleRangeBar() { AnchorValue = 7, Value = 3 });

			// markers
			//scale.Markers.Add(new ArcScaleMarker() { Value = 7 });

			gauge.Scales.Add(scale);

			TheGrid.Children.Add(gauge);
		}
	}
}
