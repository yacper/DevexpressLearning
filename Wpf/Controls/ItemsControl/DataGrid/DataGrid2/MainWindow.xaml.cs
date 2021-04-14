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
using DataGrid2.Windows;

namespace DataGrid2
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

		private void AutoGenColumns_Click(object sender, RoutedEventArgs e)
		{
			AutoGenColumnsWindow window = new AutoGenColumnsWindow();
			window.Show();
		}

		private void ManualDefineColumns_Click(object sender, RoutedEventArgs e)
		{
			ManualDefineColumnsWindow window = new ManualDefineColumnsWindow();
			window.Show();
		}

		private void Selection_Click(object sender, RoutedEventArgs e)
		{
			SelectionWindow window = new SelectionWindow();
			window.Show();
		}

		private void Sort_Click(object sender, RoutedEventArgs e)
		{
			Sort_Reorder_Resize_Window window = new Sort_Reorder_Resize_Window();
			window.Show();
		}

		private void Grouping_Click(object sender, RoutedEventArgs e)
		{
			GroupingWindow window = new GroupingWindow();
			window.Show();
		}

		private void RowDetails_Click(object sender, RoutedEventArgs e)
		{
			RowDetailsWindow window = new RowDetailsWindow();
			window.Show();
		}

		private void RowDetailsTemplateSelector_Click(object sender, RoutedEventArgs e)
		{
			RowDetailsTemplateSelectorWindow window = new RowDetailsTemplateSelectorWindow();
			window.Show();
		}


		private void AlternatingBackgroundBrush_Click(object sender, RoutedEventArgs e)
		{
			AlternatingBackgroundBrushWindow window = new AlternatingBackgroundBrushWindow();
			window.Show();
		}

		private void FrozenColumns_Click(object sender, RoutedEventArgs e)
		{
			FrozenColumnsWindow window = new FrozenColumnsWindow();
			window.Show();
		}


		private void HeadersVisibility_Click(object sender, RoutedEventArgs e)
		{
			HeadersvisbilityWindow window = new HeadersvisbilityWindow();
			window.Show();
		}



	}
}
