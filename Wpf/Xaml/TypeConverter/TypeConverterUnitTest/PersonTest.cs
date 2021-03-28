using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace TypeConverterUnitTest
{
    [TypeConverter(typeof(PersonConverter))]
	public class Person
	{
		public override string ToString()
		{
			return $"{Name}:{Age}";
		}

		public string Name { get; set; }
		public int Age { get; set; }

	}

	public class PersonConverter : TypeConverter
	{
		/// 从String转换为Person
		public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
		{
            if (value is InstanceDescriptor instanceDescriptor)
            {
                return instanceDescriptor.Invoke();
            }

            if (value is string)
            {
	            string[] ss = (value as string).Split(':');
	            return new Person() {Name = ss[0], Age = Convert.ToInt32(ss.ElementAtOrDefault(1))};
            }
		

			return null;
		}

		/// same as base
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
			Type destinationType)
		{
			if (destinationType == null)
            {
                throw new ArgumentNullException(nameof(destinationType));
            }

            if (destinationType == typeof(string))
            {
                if (value == null)
                {
                    return string.Empty;
                }

                if (culture != null && culture != CultureInfo.CurrentCulture)
                {
                    if (value is IFormattable formattable)
                    {
                        return formattable.ToString(format: null, formatProvider: culture);
                    }
                }
				// just use toString
                return value.ToString();
            }
            throw GetConvertToException(value, destinationType);

		}
	}

	class PersonTest
	{
		Person PP= new Person() {Name = "joshua", Age = 24 };
		private string PPString = "joshua:24";

		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public void ToString()
		{
			Assert.AreEqual(TypeDescriptor.GetConverter(typeof(Person)).ConvertToString(PP), "joshua:24");
		}

		[Test]
		public void FromString()
		{
			Person c = (Person)TypeDescriptor.GetConverter(typeof(Person)).ConvertFromString(PPString);
			Assert.AreEqual(c.Name, "joshua");
			Assert.AreEqual(c.Age, 24);
		}

	}
}
