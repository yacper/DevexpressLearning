using System;
using System.Collections.Generic;
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
    public partial class PathFillPage : Window
    {
        private SKElement canvasView;
        public PathFillPage()
        {
            InitializeComponent();

            Title = "PathFillPage";

            canvasView = new SKElement();
            canvasView.PaintSurface += OnPaintSurface;
            Content = canvasView;
        }


        private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();

            float width = canvasView.CanvasSize.Width;
            float height = canvasView.CanvasSize.Height;


            {
                SKPath path = new SKPath
                {
                    //FillType = (SKPathFillType)fillTypePicker.SelectedItem
                };
                path.MoveTo(0, height);
                path.LineTo(100, height - 100);
                path.LineTo(200, height - 100);
                path.LineTo(300, height - 150);
                path.LineTo(300, height);
                path.Close();

                SKPaint paint = new SKPaint
                {
                    Style = SKPaintStyle.Fill,
                    //Color = SKColors.Blue,
                };

                // Create linear gradient from upper-left to lower-right
                paint.Shader = SKShader.CreateLinearGradient(
                                    new SKPoint(0, height - 150),
                                    new SKPoint(0, height),
                                    new SKColor[] { SKColors.Blue, SKColors.White },
                                    new float[] { 0, 1 },
                                    SKShaderTileMode.Clamp);

                canvas.DrawPath(path, paint);
            }

            {
                SKPath path = new SKPath
                {
                    //FillType = (SKPathFillType)fillTypePicker.SelectedItem
                };
                path.MoveTo(400, height);
                path.LineTo(500, height - 100);
                path.LineTo(600, height - 100);
                path.LineTo(700, height - 150);
                path.LineTo(600, height - 50 );
                path.Close();

                SKPaint paint = new SKPaint
                {
                    Style = SKPaintStyle.Fill,
                    //Color = SKColors.Blue,
                };

                // Create linear gradient from upper-left to lower-right
                paint.Shader = SKShader.CreateLinearGradient(
                                    new SKPoint(0, height - 150),
                                    new SKPoint(0, height),
                                    new SKColor[] { SKColors.Blue, SKColors.White },
                                    new float[] { 0, 1 },
                                    SKShaderTileMode.Clamp);

                canvas.DrawPath(path, paint);
            }



        }

    }
}
