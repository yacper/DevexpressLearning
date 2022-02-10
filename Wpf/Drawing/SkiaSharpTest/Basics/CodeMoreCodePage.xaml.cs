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
    public partial class CodeMoreCodePage : Window
    {
        SKElement canvasView;
        bool isAnimating;
        Stopwatch stopwatch = new Stopwatch();

        private DispatcherTimer dtimer = new DispatcherTimer();
        double transparency;

        public CodeMoreCodePage()
        {
            InitializeComponent();

            Title = "Code More Code";

            canvasView = new SKElement();
            canvasView.PaintSurface += OnCanvasViewPaintSurface;
            Content = canvasView;


            Loaded += (s, e) =>
            {
                isAnimating = true;
                stopwatch.Start();


                dtimer.Interval = TimeSpan.FromMilliseconds(16);
                dtimer.Tick += OnTimerTick;
                dtimer.Start();

            };

            Unloaded += (s, e) =>
            {
                stopwatch.Stop();
                isAnimating = false;
            };

        }



        void OnTimerTick(object? s, EventArgs e)
        {
            const int duration = 5;     // seconds
            double progress = stopwatch.Elapsed.TotalSeconds % duration / duration;
            transparency = 0.5 * (1 + Math.Sin(progress * 2 * Math.PI));
            canvasView.InvalidateVisual();

        }

        void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();

            const string TEXT1 = "CODE";
            const string TEXT2 = "MORE";

            using (SKPaint paint = new SKPaint())
            {
                // Set text width to fit in width of canvas
                paint.TextSize = 100;
                float textWidth = paint.MeasureText(TEXT1);
                paint.TextSize *= 0.9f * info.Width / textWidth;

                // Center first text string
                SKRect textBounds = new SKRect();
                paint.MeasureText(TEXT1, ref textBounds);

                float xText = info.Width / 2 - textBounds.MidX;
                float yText = info.Height / 2 - textBounds.MidY;

                paint.Color = SKColors.Blue.WithAlpha((byte)(0xFF * (1 - transparency)));
                canvas.DrawText(TEXT1, xText, yText, paint);

                // Center second text string
                textBounds = new SKRect();
                paint.MeasureText(TEXT2, ref textBounds);

                xText = info.Width / 2 - textBounds.MidX;
                yText = info.Height / 2 - textBounds.MidY;

                paint.Color = SKColors.Blue.WithAlpha((byte)(0xFF * transparency));
                canvas.DrawText(TEXT2, xText, yText, paint);
            }
        }

    }
}
