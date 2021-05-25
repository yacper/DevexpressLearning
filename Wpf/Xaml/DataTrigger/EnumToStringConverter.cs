// Decompiled with JetBrains decompiler
// Type: DevExpress.Mvvm.UI.EnumToStringConverter
// Assembly: DevExpress.Xpf.Core.v20.2, Version=20.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: 246E6B96-F2C0-41CA-9DC9-63DE674FA4A3
// Assembly location: C:\Program Files (x86)\DevExpress 20.2\.NET Core Desktop Libraries\Offline Packages\devexpress.windowsdesktop.wpf.core\20.2.6\lib\netcoreapp3.0\DevExpress.Xpf.Core.v20.2.dll

using System;
using System.Globalization;
using System.Windows.Data;

namespace DevExpress.Mvvm.UI
{
  public class EnumToStringConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value == null ? (object) null : (object) value.ToString();

    public object ConvertBack(
      object value,
      Type targetType,
      object parameter,
      CultureInfo culture)
    {
      return value == null || !targetType.IsEnum ? value : Enum.Parse(targetType, value.ToString());
    }
  }
}
