using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Serialization;
using DevExpress.Xpf.Docking;
using DevExpress.Xpf.Layout.Core;
using DockLayoutManagerPractice.Views;

namespace DockLayoutManagerPractice
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow:Window
	{
		public MainWindow()
		{
			InitializeComponent();


			if(File.Exists("test.xml"))
			{
				//WorkspaceManager workspaceManager = WorkspaceManager.GetWorkspaceManager(docklayoutmanager1) as WorkspaceManager;
				//workspaceManager.LoadWorkspace("workspace1", "test.xml");
				//workspaceManager.ApplyWorkspace("workspace1");

				docklayoutmanager1.DockItemRestoring += (a, b) =>
				{

				};
				docklayoutmanager1.DockItemRestored += (a, b) =>
				{

				};



				docklayoutmanager1.LayoutItemRestored += (a, b) =>
				{


				};


				// 1. restore 只会还原layout，不会创建其中的content
				docklayoutmanager1.RestoreLayoutFromXml("test.xml");

				//foreach (var item in docklayoutmanager1.Items)
				//{
				//	Debug.WriteLine(item);
					
				//}

				foreach (var item in docklayoutmanager1.LayoutRoot.Items)
				{
					Debug.WriteLine(item);
					
				}


				var containerList  = DevExpress.Mvvm.UI.LayoutTreeHelper.GetVisualChildren(this).OfType<LayoutPanel>();

				// 2. 当layout还原的时候，可以layoutpanel的tag的属性，还原layoutPanel的Content控件
				var containerList1 = DevExpress.Mvvm.UI.LayoutTreeHelper.GetLogicalChildren(this).OfType<LayoutPanel>();
				foreach (LayoutPanel item in containerList1)
				{
					Debug.WriteLine(item);
					Debug.WriteLine($"Panel Name:{item.Name} Tag:{item.Tag}");
					if(item.Name == "ErrorList")
						(item as LayoutPanel).Content = new ErrorList();
				}


				return;
				// 这种也可以
				foreach (FloatGroup fg in docklayoutmanager1.FloatGroups)
				{
					Debug.WriteLine(fg);
					foreach (BaseLayoutItem item in fg.Items)
					{
						Debug.WriteLine(item);
						Debug.WriteLine($"Panel Name:{item.Name} Tag:{item.Tag}");
						if (item.Name == "ErrorList")
							(item as LayoutPanel).Content = new ErrorList();
					}
				}


				//var containerList = DevExpress.Mvvm.UI.LayoutTreeHelper.GetVisualChildren(docklayoutmanager1).OfType<LayoutPanel>();
				//var docList = DevExpress.Mvvm.UI.LayoutTreeHelper.GetLogicalChildren(docklayoutmanager1).OfType<LayoutPanel>();

				//foreach (var VARIABLE in docklayoutmanager1.LayoutRoot.chi)
				//{
					
				//}

			}
			else
			{


				//LayoutControlItem lc    = new LayoutControlItem();
				//lc.Content = new ErrorList();
				var               panel = docklayoutmanager1.DockController.AddPanel(DockType.None);
				panel.Caption = "ErrorList";
				panel.Name    = "ErrorList";
				panel.Tag     = "ErrorList 1:abc";
				panel.Content = new ErrorList();
				//DXSerializer.SetSerializationID(panel.Content as DependencyObject, "ErrorList");
				//var               panel2 = docklayoutmanager1.DockController.AddPanel(DockType.None);
				//panel2.Caption = "ErrorList";
				//panel2.Name = "ErrorList2";
				//panel2.Content = new ErrorList();
				//DXSerializer.SetSerializationID(panel2.Content as DependencyObject, "ErrorList2");
	
				FloatGroup floatgroup = new FloatGroup();
				floatgroup.Name = "TestFloatGroup";
				floatgroup.FloatSize = new Size(500, 500);
				floatgroup.FloatLocation = new Point(300, 300);
				floatgroup.Add(panel);
				//floatgroup.Add(panel2);
				docklayoutmanager1.FloatGroups.Add(floatgroup);
				docklayoutmanager1.BeginUpdate();

			}
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{

			string xmlFilePath = System.AppDomain.CurrentDomain.BaseDirectory + "test.xml";

				//WorkspaceManager workspaceManager = WorkspaceManager.GetWorkspaceManager(docklayoutmanager1) as WorkspaceManager;
				//workspaceManager.CaptureWorkspace("workspace1");
				//workspaceManager.SaveWorkspace("workspace1", "test.xml");


				//return;

			if(File.Exists("test.xml"))
			{
				docklayoutmanager1.SaveLayoutToXml("test.xml");

			}
			else
			{
				using(File.Create("test.xml")) { }
				docklayoutmanager1.SaveLayoutToXml("test.xml");

		}
		}
	}
}
