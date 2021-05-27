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
using GalaSoft.MvvmLight;
using Binding = System.Windows.Data.Binding;

namespace Binding
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private Student stu;

		public MainWindow()
		{
			InitializeComponent();



			stu = new Student();
			// Binding 
			System.Windows.Data.Binding binding = new System.Windows.Data.Binding();
			binding.Source = stu;
			binding.Path = new PropertyPath("Name");

			// Bind数据源和目标
			BindingOperations.SetBinding(this.textBoxName, TextBox.TextProperty, binding);

			// 或者
			//this.textBoxName.SetBinding(TextBox.TextProperty, new System.Windows.Data.Binding("Name") { Source = stu = new Student() }); 

			DataContext = stu;
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			stu.Name+= "Name"; 
		}

		private void load_click(object sender, RoutedEventArgs e)
		{
			List<Plane> planeList = new List<Plane>()
									{
										new Plane() {Category = Category.Bomber, Name  = "B-2", State = State.Unknown},
										new Plane() {Category = Category.Bomber, Name  = "B-2", State  = State.Available},
										new Plane() {Category = Category.Fighter, Name = "F-22", State = State.Locked},
										new Plane() { Category = Category.Fighter, Name = "Su-47",  State= State.Available },
										new Plane() {Category = Category.Bomber, Name = "B-52", State = State.Unknown},
										new Plane() {Category = Category.Fighter, Name  = "B-2", State = State.Unknown}
									};

			this.listBoxPlane.ItemsSource = planeList;
		}

		private void save_click(object sender, RoutedEventArgs e)
		{
			StringBuilder sb = new StringBuilder();
			foreach (Plane p in listBoxPlane.Items)
			{
				sb.AppendLine(string.Format($"Category={p.Category}, Name={p.Name}, State={p.State}"));
			}
			File.WriteAllText(@"D:\planlist.txt", sb.ToString());
		}
	}


	public class Student:ObservableObject
	{
		protected string _Name;

		public string Name
		{
			get
			{
				return _Name;
			}
			set
			{
				if (value == Name) return;
				_Name = value;

				RaisePropertyChanged();
			}
		}
	}


	public enum Category 
	{
		Bomber,
		Fighter
	}
	public enum State
	{
		Available,
		Locked,
		Unknown
	}
	public class Plane
	{
		public Category Category { get; set; }
		public string Name { get; set; }
		public State State { get; set; }
	}



}