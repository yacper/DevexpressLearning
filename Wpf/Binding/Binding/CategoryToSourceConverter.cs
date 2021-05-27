using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Binding
{
	public class CategoryToSourceConverter : IValueConverter
	{
		// Category To Uri 
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			Category c = (Category)value;
			switch (c)
			{
				case Category.Bomber:
					return @"/Icons/Bomber.png";
				case Category.Fighter:
					return @"/Icons/Fighter.png";
				default:
					return null;
			}
		}

		// wont't be called
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
