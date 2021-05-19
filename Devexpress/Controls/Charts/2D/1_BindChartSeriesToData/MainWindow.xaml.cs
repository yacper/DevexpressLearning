using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace _1_BindChartSeriesToData
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		public MainWindow() { InitializeComponent(); }
	}

  public class MainWindowViewModel {
        public ObservableCollection<DataPoint> Data { get; private set; }
        public MainWindowViewModel() {
            this.Data = DataPoint.GetDataPoints();
        }
    }

	public class DataPoint
	{
		public string Argument { get; set; }
		public double Value    { get; set; }

		public static ObservableCollection<DataPoint> GetDataPoints()
		{
			return new ObservableCollection<DataPoint>
			       {
				       new DataPoint {Argument = "Asia", Value          = 5.289D},
				       new DataPoint {Argument = "Australia", Value     = 2.2727D},
				       new DataPoint {Argument = "Europe", Value        = 3.7257D},
				       new DataPoint {Argument = "North America", Value = 4.1825D},
				       new DataPoint {Argument = "South America", Value = 2.1172D}
			       };
		}
	}
}