using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

            ObservableCollection<Customer> customers = new ObservableCollection<Customer>();
            for (int i = 1; i < 30; i++)
            {
                customers.Add(new Customer() { ID = i, Name = "Name" + i });
            }
            gridControl1.ItemsSource = customers;

            
        }

        public string workspacePath = "Layout.xml";
		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			WorkspaceManager workspaceManager = WorkspaceManager.GetWorkspaceManager(dockLayoutManager) as WorkspaceManager;
            workspaceManager.CaptureWorkspace("workspace1");
            workspaceManager.SaveWorkspace("workspace1", workspacePath);		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			WorkspaceManager workspaceManager = WorkspaceManager.GetWorkspaceManager(dockLayoutManager) as WorkspaceManager;
			workspaceManager.LoadWorkspace("workspace1", workspacePath);
			workspaceManager.ApplyWorkspace("workspace1");
		}


    }


    public class Customer
    {
        public int ID
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }
    }
}
