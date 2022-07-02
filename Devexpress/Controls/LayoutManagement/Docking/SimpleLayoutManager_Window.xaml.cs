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
using DevExpress.Pdf.Native;
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


				// 注意:dxdo:RestoreLayoutOptions.RemoveOldPanels="False" 该选项是必须的，不然保存的layout不会重新创建
				// dxdo:RestoreLayoutOptions.AddNewPanels="False" 该选项也需要，不然已存在的layout不会销毁，而会存在于保存的xml中
				layoutManager.RestoreLayoutFromXml(LayoutPath);
            else
            {
                layoutManager.LayoutRoot = new LayoutGroup() { Name = "root", Orientation = Orientation.Vertical };
                LayoutPanel upPanel = new LayoutPanel() { Name = "upPanel", Caption = "UpPanel" };
                LayoutPanel dnPanel = new LayoutPanel() { Name="dnPanel", Caption = "dnPanel" };
				layoutManager.LayoutRoot.Add(upPanel);
				layoutManager.LayoutRoot.Add(dnPanel);

            }
		}
	}
}
