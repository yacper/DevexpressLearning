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
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using SkiaSharp.Views.WPF;

namespace SkiaSharpTest
{
    /// <summary>
    /// SkElementPage.xaml 的交互逻辑
    /// </summary>
    public partial class PulsatingEllipsePage : Window
    {

        Stopwatch stopwatch = new Stopwatch();
        bool pageIsActive;
        float scale;            // ranges from 0 to 1 to 0

        public PulsatingEllipsePage()
        {
            InitializeComponent();

            Loaded += (s, e) =>
            {
                pageIsActive = true;
                AnimationLoop();
            };

            Unloaded += (s, e) =>
            {
                pageIsActive = false;

            };

        }


        async Task AnimationLoop()
        {
            stopwatch.Start();

            while (pageIsActive)
            {
                double cycleTime = slider.Value;
                double t = stopwatch.Elapsed.TotalSeconds % cycleTime / cycleTime;
                scale = (1 + (float)Math.Sin(2 * Math.PI * t)) / 2;
                canvasView.InvalidateVisual();
                await Task.Delay(TimeSpan.FromSeconds(1.0 / 30));
            }
            stopwatch.Stop();
        }

        void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();

            float maxRadius = 0.75f * Math.Min(info.Width, info.Height) / 2;
            float minRadius = 0.25f * maxRadius;

            float xRadius = minRadius * scale + maxRadius * (1 - scale);
            float yRadius = maxRadius * scale + minRadius * (1 - scale);

            using (SKPaint paint = new SKPaint())
            {
                paint.Style = SKPaintStyle.Stroke;
                paint.Color = SKColors.Blue;
                paint.StrokeWidth = 50;
                canvas.DrawOval(info.Width / 2, info.Height / 2, xRadius, yRadius, paint);

                paint.Style = SKPaintStyle.Fill;
                paint.Color = SKColors.SkyBlue;
                canvas.DrawOval(info.Width / 2, info.Height / 2, xRadius, yRadius, paint);
            }
        }

    }
}
