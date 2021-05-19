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

namespace Gauges.Linear_Gauges
{
    /// <summary>
    /// LinearGauge_CustomElement_Window.xaml 的交互逻辑
    /// </summary>
    public partial class LinearGauge_CustomElement_Window : Window
    {
        public LinearGauge_CustomElement_Window()
        {
            InitializeComponent();
		}

		private void button1_Click(object sender, RoutedEventArgs e)
		{
			if (bar.Value < 100)
				bar.Value += 10;
		}

		private void button2_Click(object sender, RoutedEventArgs e)
		{
			if (bar.Value > 0)
				bar.Value -= 10;
		}
	}
}
