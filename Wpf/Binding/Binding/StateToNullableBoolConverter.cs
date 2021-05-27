using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Binding
{
	public class StateToNullableBoolConverter : IValueConverter
	{
		// State to bool? 
		public Object Convert(object value, Type targetType, Object parameter, CultureInfo culture)
		{
			State s = (State)value;
			switch (s)
			{

				case State.Locked:
					return false;
				case State.Available:
					return true;
				case State.Unknown:
				default:
					return null;
			}
		}
		// bool? to State 
		public Object ConvertBack(object value, Type targetType, Object parameter, CultureInfo culture)
		{
			bool? nb = (bool?)value;
			{
				switch (nb)
				{
					case true:
						return State.Available;
					case false:
						return State.Locked;
					case null:
					default:
						return State.Unknown;
				}
			}
		}
	}
}
