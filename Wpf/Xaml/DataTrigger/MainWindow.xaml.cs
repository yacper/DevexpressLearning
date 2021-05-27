using DotEx.Common;
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

namespace DataTrigger
{
	public enum CheckerState
	{
		None = 0,
		Passed = 1,
		Warn = 2,
		Fatal = 3,
	}

	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			DataContext = this;


			int[] a = {1, 2, 3};
			int[] b = { 2, 3, 4};

			List<int> aa = a.Except(b, p => p, (a,b)=>a==b).ToList();
			List<int> bb = b.Except(a, p => p, (a,b)=>a==b).ToList();


		}


		public virtual CheckerState SystemStat { get; set; } = CheckerState.Warn;
	}
}
