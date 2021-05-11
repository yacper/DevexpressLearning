using Google.Protobuf.WellKnownTypes;
using System;
using System.Windows.Data;
using System.Windows.Markup;

class TimestampConverter : MarkupExtension, IValueConverter
{
	public TimestampConverter() { }

	public override object ProvideValue(IServiceProvider serviceProvider)
	{
		return this;
	}

	public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
	{
		return value != null && (value as Timestamp).ToDateTime().Year > 1900 ? (value as Timestamp).ToDateTime().ToLocalTime() : (object)null;
	}
	public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
	{
		if (value == null) return null;
		return  Timestamp.FromDateTime(((DateTime)value).ToUniversalTime());
	}
}
