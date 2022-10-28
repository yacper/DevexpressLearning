/********************************************************************
    created:	2022/2/9 17:26:31
    author:		rush
    email:		yacper@gmail.com	
	
    purpose:
    modifiers:	
*********************************************************************/
using DevExpress.Xpf.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using RLib.Base;

namespace NeoTrader{
    public static class ImagePaths
    {
        const string folderSvg = "pack://application:,,,/NeoTrader;component/Resources/Images/Docking/";
        const string folder = "/NeoTrader;component/Resources/Images/Docking/";
        const string DxFloder = "pack://application:,,,/DevExpress.Images.v22.1;component/";
        const string ImageFloder = "pack://application:,,,/NeoControls;component/Resources/Images/";

        public const string Watchlist = $"{ImageFloder}Panel/Watchlist.svg";
        public const string Monitor = $"{ImageFloder}Panel/Monitor.svg";
        public const string Logger = $"{ImageFloder}Panel/Logger.svg";
        public const string Trading = $"{ImageFloder}Panel/Trading.svg";
        public const string Account = $"{ImageFloder}Panel/Account.svg";
        public const string AlertWindow = ImageFloder + "Panel/Alert.svg";


        public const string ConnectedStatus = ImageFloder + "Panel/ConnectedStatus.svg";
        public const string ConnectingStatus = ImageFloder + "Panel/ConnectingStatus.svg";
       
    }

    public static class Images
    {
        public static ImageSource Watchlist { get { return GetSvgImage(ImagePaths.Watchlist); } }
        public static ImageSource Monitor { get { return GetSvgImage(ImagePaths.Monitor); } }
        public static ImageSource Logger { get { return GetSvgImage(ImagePaths.Logger); } }
        public static ImageSource Trading { get { return GetSvgImage(ImagePaths.Trading); } }
        public static ImageSource Account { get { return GetSvgImage(ImagePaths.Account); } }


        public static ImageSource ConnectedStatus { get { return GetSvgImage(ImagePaths.ConnectedStatus); } }
        public static ImageSource ConnectingStatus { get { return GetSvgImage(ImagePaths.ConnectingStatus); } }

        static ImageSource GetImage(string path)
        {
            return new BitmapImage(new Uri(path, UriKind.Relative));
        }

        static ImageSource GetSvgImage(string path)
        {
            var svgImageSource = new SvgImageSourceExtension() { Uri = new Uri(path) }.ProvideValue(null);
            return (ImageSource)svgImageSource;
        }

    }
 
}
