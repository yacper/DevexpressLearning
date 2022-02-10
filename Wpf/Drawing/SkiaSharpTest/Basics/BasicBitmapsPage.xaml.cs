using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using SkiaSharp.Views.WPF;

namespace SkiaSharpTest
{
    /// <summary>
    /// SkElementPage.xaml 的交互逻辑
    /// </summary>
    public partial class BasicBitmapsPage : Window
    {
        SKElement canvasView;
        HttpClient httpClient = new HttpClient();

        SKBitmap webBitmap;
        SKBitmap resourceBitmap;
        SKBitmap libraryBitmap;


        public BasicBitmapsPage()
        {
            InitializeComponent();

            Title = "Basic Bitmaps";

            canvasView = new SKElement();
            canvasView.PaintSurface += OnPaintSurface;
            Content = canvasView;

             // Load resource bitmap
            Assembly assembly = GetType().GetTypeInfo().Assembly;
            string resourceID = assembly.GetName().Name+ ".Resources.monkey.png";

            using (Stream stream = assembly.GetManifestResourceStream(resourceID))
            {
                resourceBitmap = SKBitmap.Decode(stream);
            }

            //// Add tap gesture recognizer
            //TapGestureRecognizer tapRecognizer = new TapGestureRecognizer();
            //tapRecognizer.Tapped += async (sender, args) =>
            //{
            //    // Load bitmap from photo library
            //    IPhotoLibrary photoLibrary = DependencyService.Get<IPhotoLibrary>();

            //    using (Stream stream = await photoLibrary.PickPhotoAsync())
            //    {
            //        if (stream != null)
            //        {
            //            libraryBitmap = SKBitmap.Decode(stream);
            //            canvasView.InvalidateSurface();
            //        }
            //    }
            //};
            //canvasView.GestureRecognizers.Add(tapRecognizer);




            Loaded += async (s, e) =>
            {
                // Load web bitmap.
                string url = "https://images2015.cnblogs.com/blog/694927/201603/694927-20160322151809854-952396293.png";

                try
                {
                    using (Stream stream = await httpClient.GetStreamAsync(url))
                    using (MemoryStream memStream = new MemoryStream())
                    {
                        await stream.CopyToAsync(memStream);
                        memStream.Seek(0, SeekOrigin.Begin);

                        webBitmap = SKBitmap.Decode(memStream);
                        canvasView.InvalidateVisual();
                    }
                }
                catch
                {
                }

            };
            Unloaded += (s, e) =>
            {

            };

        }


        private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();

            if (webBitmap != null)
            {
                float x = (info.Width - webBitmap.Width) / 2;
                float y = (info.Height / 3 - webBitmap.Height) / 2;
                canvas.DrawBitmap(webBitmap, x, y);
            }

            if (resourceBitmap != null)
            {
                canvas.DrawBitmap(resourceBitmap, 
                    new SKRect(0, info.Height / 3, info.Width, 2 * info.Height / 3));
            }

            if (libraryBitmap != null)
            {
                float scale = Math.Min((float)info.Width / libraryBitmap.Width,
                                       info.Height / 3f / libraryBitmap.Height);

                float left = (info.Width - scale * libraryBitmap.Width) / 2;
                float top = (info.Height / 3 - scale * libraryBitmap.Height) / 2;
                float right = left + scale * libraryBitmap.Width;
                float bottom = top + scale * libraryBitmap.Height;
                SKRect rect = new SKRect(left, top, right, bottom);
                rect.Offset(0, 2 * info.Height / 3);

                canvas.DrawBitmap(libraryBitmap, rect);
            }
            else
            {
                using (SKPaint paint = new SKPaint())
                {
                    paint.Color = SKColors.Blue;
                    paint.TextAlign = SKTextAlign.Center;
                    paint.TextSize = 48;

                    canvas.DrawText("Tap to load bitmap", 
                        info.Width / 2, 5 * info.Height / 6, paint);
                }
            }
        }

    }
}
