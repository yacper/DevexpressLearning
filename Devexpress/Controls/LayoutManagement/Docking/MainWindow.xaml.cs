using System;
using System.Collections.Generic;
using System.IO;
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
using DevExpress.Xpf.Core.Serialization;

namespace Layout
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

		public string LayoutPath = "Layout.xml";
		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			//Save the layout of serializable DevExpress controls starting with the current object (specified by "this").
			//If the current object ("this") is also a serializable DevExpress control, its layout is saved as well.
			DXSerializer.Serialize(this, LayoutPath, "app", null);
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			return;
			if (File.Exists(LayoutPath))
				DXSerializer.Deserialize(this, LayoutPath, "app", null);
		}
	}
}
