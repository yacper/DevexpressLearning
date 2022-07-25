using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Cars
{
//厂商名称转换为Logo路径
public class AutoMarkToLogoPathConverter : IValueConverter
{
    /// <summary>
    /// 正向转
    /// </summary>
    /// <param name="value"></param>
    /// <param name="targetType"></param>
    /// <param name="parameter"></param>
    /// <param name="culture"></param>
    /// <returns></returns>
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        var image = new BitmapImage();

        image.BeginInit();
        image.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
        image.CacheOption   = BitmapCacheOption.OnLoad;
        image.UriSource     = new Uri($"Resource/Image/{value}.png", UriKind.Relative);
        image.EndInit();

        return image;

        // 这样也可以
        var bi = new BitmapImage();

        using (var fs = new FileStream($"Resource/Image/{value}.png", FileMode.Open))
        {
            bi.BeginInit();
            bi.StreamSource = fs;
            bi.CacheOption  = BitmapCacheOption.OnLoad;
            bi.EndInit();
        }

        bi.Freeze(); //Important to freeze it, otherwise it will still have minor leaks

        return bi;


        //这种方式load不及时
        var img = new BitmapImage(new Uri($"Resource/Image/{value}.png", UriKind.Relative), new RequestCachePolicy(RequestCacheLevel.Reload));
        //var img =  new BitmapImage(new Uri($"Resource/Image/{value}.png", UriKind.Relative), new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore)); // 这样也不行
        return img;
    }

    /// <summary>
    /// 逆向转未用到
    /// </summary>
    /// <param name="value"></param>
    /// <param name="targetType"></param>
    /// <param name="parameter"></param>
    /// <param name="culture"></param>
    /// <returns></returns>
    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) { throw new NotImplementedException(); }
}
}