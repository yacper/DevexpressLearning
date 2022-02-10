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
    public partial class BitmapDissolvePage : Window
    {
        SKBitmap bitmap1;
        SKBitmap bitmap2;

        public BitmapDissolvePage()
        {
            InitializeComponent();

            // Load two bitmaps
            Assembly assembly = GetType().GetTypeInfo().Assembly;

            using (Stream stream = assembly.GetManifestResourceStream(
                                    "SkiaSharpTest.Resources.SeatedMonkey.jpg"))
            {
                bitmap1 = SKBitmap.Decode(stream);
            }
            using (Stream stream = assembly.GetManifestResourceStream(
                                    "SkiaSharpTest.Resources.FacePalm.jpg"))
            {
                bitmap2 = SKBitmap.Decode(stream);
            }

        }
      

        void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();

            // Find rectangle to fit bitmap
            float scale = Math.Min((float)info.Width / bitmap1.Width,
                                   (float)info.Height / bitmap1.Height);
            SKRect rect = SKRect.Create(scale * bitmap1.Width,
                                        scale * bitmap1.Height);
            float x = (info.Width - rect.Width) / 2;
            float y = (info.Height - rect.Height) / 2;
            rect.Offset(x, y);

            // Get progress value from Slider
            float progress = (float)progressSlider.Value;

            // Display two bitmaps with transparency
            using (SKPaint paint = new SKPaint())
            {
                paint.Color = paint.Color.WithAlpha((byte)(0xFF * (1 - progress)));
                canvas.DrawBitmap(bitmap1, rect, paint);

                paint.Color = paint.Color.WithAlpha((byte)(0xFF * progress));
                canvas.DrawBitmap(bitmap2, rect, paint);
            }
        }

        private void ProgressSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            canvasView.InvalidateVisual();
        }
    }
}
