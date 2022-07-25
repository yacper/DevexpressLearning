using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Cars
{
    //汽车名称转换为照片路径
    public class NameToPhotoPathConverter:IValueConverter
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
            image.UriSource     = new Uri($"Resource/Image/{value}.jpg", UriKind.Relative);
            image.EndInit();

            return image;



            // 来不及加载
            return new BitmapImage(new Uri(string.Format(@"Resource/Image/{0}.jpg", (string)value), UriKind.Relative));
        }
        /// <summary>
        /// 逆向转未用到
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
