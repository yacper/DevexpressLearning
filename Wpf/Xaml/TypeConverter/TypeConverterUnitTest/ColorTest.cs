using System;
using System.ComponentModel;
using System.Drawing;
using NUnit.Framework;

namespace TypeConverterUnitTest
{
	public class Tests
	{
		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public void ToString()
		{
			Color c = Color.Red;
			Assert.AreEqual(TypeDescriptor.GetConverter(typeof(Color)).ConvertToString(c), "Red");
		}

		[Test]
		public void FromString()
		{
			Color c = (Color)TypeDescriptor.GetConverter(typeof(Color)).ConvertFromString("Red");
			Assert.AreEqual(c, Color.Red);
		}

		[Test]
		public void GetStandardValues()
		{
			foreach (Color c in TypeDescriptor.GetConverter(typeof(Color)).GetStandardValues())
			{
				System.Diagnostics.Debug.WriteLine(TypeDescriptor.GetConverter(c).ConvertToString(c));
			}

		}
	}
}