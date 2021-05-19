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

namespace Gauges.Circular_Gauges
{
    /// <summary>
    /// CircularGaugeByCode_Window.xaml 的交互逻辑
    /// </summary>
    public partial class CircularGaugeByCode_Window : Window
    {
        public CircularGaugeByCode_Window()
        {
            InitializeComponent();




            CircularGaugeControl gauge = new CircularGaugeControl();
            ArcScale scale = new ArcScale()
                             {
                                 StartValue = 0,
                                 EndValue = 12,
                                 StartAngle = -90,
                                 EndAngle = 270,
                                 MajorIntervalCount = 12,
                                 MinorIntervalCount = 5
                             };

            // needles
            scale.Needles.Add(new ArcScaleNeedle(){Value = 3});
            scale.Needles.Add(new ArcScaleNeedle(){Value = 12});
            scale.Needles.Add(new ArcScaleNeedle(){Value = 5});

            // layer
            scale.Layers.Add(new ArcScaleLayer());

	        // ranges
            scale.Ranges.Add(new ArcScaleRange(){StartValue = new RangeValue(0), EndValue = new RangeValue(4)});
            scale.Ranges.Add(new ArcScaleRange(){StartValue = new RangeValue(4), EndValue = new RangeValue(8)});
            scale.Ranges.Add(new ArcScaleRange(){StartValue = new RangeValue(8), EndValue = new RangeValue(12)});

            // range bar
            scale.RangeBars.Add(new ArcScaleRangeBar(){AnchorValue = 7, Value = 3});

            // markers
            scale.Markers.Add(new ArcScaleMarker(){Value = 7});

            gauge.Scales.Add(scale);

            TheGrid.Children.Add(gauge);

        }
    }
}
