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

namespace Button
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}


		void OnClick1(object sender, RoutedEventArgs e)
		{
			btn1.Background = Brushes.LightBlue;
		}

		void OnClick2(object sender, RoutedEventArgs e)
		{
			btn2.Background = Brushes.Pink;
		}

		void OnClick3(object sender, RoutedEventArgs e)
		{
			btn1.Background = Brushes.Pink;
			btn2.Background = Brushes.LightBlue;
		}


		void OnClick5(object sender, RoutedEventArgs e)
		{
			btn6.FontSize = 16;
			btn6.Content = "This is my favorite photo.";
			btn6.Background = Brushes.Red;
		}


		private void button1_Click(object sender, RoutedEventArgs e)
		{
			//ObjectDataProvider odp = new ObjectDataProvider();
			//odp.ObjectInstance = new Caculate();
			//odp.MethodName="Add";
			//odp.MethodParameters.Add("100");
			//odp.MethodParameters.Add("200");
			//MessageBox.Show(odp.Data.ToString());

			MessageBox.Show("hello, world");

		}
	}
}
