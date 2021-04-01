using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using DevExpress.Mvvm.UI;

namespace DXPropertyGrid
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


	}


	public class ViewModel
	{
		public Container Data { get; set; }
		public ViewModel()
		{
			Data = new Container
			{
				Simple = new ClassWithProperties() { Id = 0, Name = "Simple" },
				Expandable = new ClassWithProperties() { Id = 1, Name = "Expandable" },
				NotExpandable = new ClassWithProperties() { Id = 2, Name = "Not expandable" },
				Collection = new List<ClassWithProperties>
			{
				new ClassWithProperties() { Id=3, Name="Item1" },
				new ClassWithProperties() { Id=4, Name="Item2" }
			}
			};
		}
	}


		//[TypeConverter(typeof(ExpandableObjectConverter))]
	public class ClassWithProperties
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public override string ToString()
		{
			return Name;
		}
	}

	public class Container
	{
		public ClassWithProperties Simple { get; set; }
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public ClassWithProperties Expandable { get; set; }
		[TypeConverter(typeof(NotExpandableConverter))]
		public ClassWithProperties NotExpandable { get; set; }
		public List<ClassWithProperties> Collection { get; set; }
	}

	public class NotExpandableConverter : TypeConverter
	{
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return false;
		}
	}


}
