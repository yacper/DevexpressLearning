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
using Gauges.Circular_Gauges;
using Gauges.Linear_Gauges;

namespace Gauges
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

		public void LinearScale_Click(object sender, EventArgs e)
		{
			LinearScale_Window win = new LinearScale_Window();
				win.Show();
		}
		public void LinearScaleByCode_Click(object sender, EventArgs e)
		{
			//CircularGaugeByCode_Window win = new CircularGaugeByCode_Window();
			LinearGaugeByCode_Window win = new LinearGaugeByCode_Window();
				win.Show();
		}


		public void LinearGauge_CustomElement_Click(object sender, EventArgs e)
		{
			LinearGauge_CustomElement_Window win = new LinearGauge_CustomElement_Window();
				win.Show();
		}

	}
}
