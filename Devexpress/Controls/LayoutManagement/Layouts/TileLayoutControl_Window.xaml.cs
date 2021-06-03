using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Layouts
{
	/// <summary>
	/// TileLayoutControl_Window.xaml 的交互逻辑
	/// </summary>
	public partial class TileLayoutControl_Window : Window
	{
		public TileLayoutControl_Window()
		{
			InitializeComponent();
		}
        public List<Agent> Agents { get { return Layouts.Agents.DataSource; } }
	}


	public class Agent
	{
		public string AgentName { get; set; }
		public string Phone { get; set; }
		public string Photo { get; set; }
		public ImageSource PhotoSource
		{
			get
			{
				return string.IsNullOrEmpty(Photo) ? null : new BitmapImage(new Uri(Photo, UriKind.Relative));
			}
		}
	}

	public static class Agents
	{
		public static readonly List<Agent> DataSource =
			new List<Agent> {
				new Agent { AgentName = "Anthony Peterson", Phone = "(555) 444-0782", Photo = "Images/1.jpg" },
				new Agent { AgentName = "Rachel Scott", Phone = "(555) 249-1614", Photo = "Images/2.jpg" },
				new Agent { AgentName = "Albert Walker", Phone = "(555) 232-2303", Photo = "Images/3.jpg" }
			};
	}



}
