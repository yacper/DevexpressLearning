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
using System.Windows.Shapes;
using DevExpress.Xpf.Core.Serialization;
using DevExpress.Xpf.Docking;

namespace Layout
{
	/// <summary>
	/// SimpleLayoutManager_Window.xaml 的交互逻辑
	/// </summary>
	public partial class SimpleLayoutManager_Window : Window
	{
		public SimpleLayoutManager_Window()
		{
			InitializeComponent();
		}

		public string LayoutPath = "SimpleLayout.xml";
		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			//Save the layout of serializable DevExpress controls starting with the current object (specified by "this").
			//If the current object ("this") is also a serializable DevExpress control, its layout is saved as well.

			// 两者效果一样
			//DXSerializer.Serialize(this, LayoutPath, "app", null);
			layoutManager.SaveLayoutToXml(LayoutPath);
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			if (File.Exists(LayoutPath))
				// 两者效果一样
				//DXSerializer.Deserialize(this, LayoutPath, "app", null);
				layoutManager.RestoreLayoutFromXml(LayoutPath);
		}
	}
}
