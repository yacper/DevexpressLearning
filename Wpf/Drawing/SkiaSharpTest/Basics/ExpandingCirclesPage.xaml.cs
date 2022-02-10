using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
    public partial class ExpandingCirclesPage : Window
    {
        const double cycleTime = 1000;       // in milliseconds

        SKElement canvasView;
        Stopwatch stopwatch = new Stopwatch();
        private DispatcherTimer dtimer = new DispatcherTimer();
        bool pageIsActive;
        float t;
        SKPaint paint = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            IsAntialias = true
        };


        public ExpandingCirclesPage()
        {
            InitializeComponent();

            Title = "Framed Text";

            canvasView = new SKElement();
            canvasView.PaintSurface += OnPaintSurface;
            Content = canvasView;

            Loaded += (s, e) =>
            {
                pageIsActive = true;
                stopwatch.Start();

                dtimer.Interval = TimeSpan.FromMilliseconds(33);
                dtimer.Tick += (s, e) =>
                    {
                        t = (float)(stopwatch.Elapsed.TotalMilliseconds % cycleTime / cycleTime);
                        this.canvasView.InvalidateVisual();
                        //canvasView.InvalidateSurface();

                        if (!pageIsActive)
                        {
                            stopwatch.Stop();
                        }
                        //return pageIsActive;

                    }
                    ;
                dtimer.Start();

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

            SKPoint center = new SKPoint(info.Width / 2, info.Height / 2);
            float baseRadius = Math.Min(info.Width, info.Height) / 12;

            for (int circle = 0; circle < 5; circle++)
            {
                float radius = baseRadius * (circle + t);

                paint.StrokeWidth = baseRadius / 2 * (circle == 0 ? t : 1);
                paint.Color = new SKColor(0, 0, 255,
                    (byte)(255 * (circle == 4 ? (1 - t) : 1)));

                canvas.DrawCircle(center.X, center.Y, radius, paint);
            }
        }

    }
}
