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
    public partial class AnimatedBitmapTilePage : Window
    {

        const int SIZE = 64;

        SKElement canvasView;
        SKBitmap bitmap = new SKBitmap(SIZE, SIZE);
        float angle;

        // For animation
        bool isAnimating;
        Stopwatch stopwatch = new Stopwatch();
        
      
        public AnimatedBitmapTilePage()
        {
            InitializeComponent();

            Title = "Animated Bitmap Tile";

            // Initialize bitmap prior to animation
            DrawBitmap();

            // Create SKCanvasView 
            canvasView = new SKElement();
            canvasView.PaintSurface += OnCanvasViewPaintSurface;
            Content = canvasView;

            Loaded += (s, e) =>
            {
                isAnimating = true;
                stopwatch.Start();
                //Device.StartTimer(TimeSpan.FromMilliseconds(16), OnTimerTick);

            };

            Unloaded += (s, e) =>
            {

                stopwatch.Stop();
                isAnimating = false;
            };


        }



        bool OnTimerTick()
        {
            const int duration = 10;     // seconds
            angle = (float)(360f * (stopwatch.Elapsed.TotalSeconds % duration) / duration);
            DrawBitmap();
            canvasView.InvalidateVisual();

            return isAnimating;
        }

        void DrawBitmap()
        {
            using (SKCanvas canvas = new SKCanvas(bitmap))
            using (SKPaint paint = new SKPaint())
            {
                paint.Style = SKPaintStyle.Stroke;
                paint.Color = SKColors.Blue;
                paint.StrokeWidth = SIZE / 8;

                canvas.Clear();
                canvas.Translate(SIZE / 2, SIZE / 2);
                canvas.RotateDegrees(angle);
                canvas.DrawLine(-SIZE, -SIZE, SIZE, SIZE, paint);
                canvas.DrawLine(-SIZE, SIZE, SIZE, -SIZE, paint);
            }
        }

        void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();

            using (SKPaint paint = new SKPaint())
            {
                paint.Shader = SKShader.CreateBitmap(bitmap,
                                                     SKShaderTileMode.Mirror,
                                                     SKShaderTileMode.Mirror);
                canvas.DrawRect(info.Rect, paint);
            }
        }
    }
}
