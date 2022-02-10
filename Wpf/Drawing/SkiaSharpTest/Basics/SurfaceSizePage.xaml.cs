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

namespace SkiaSharpTest
{
    /// <summary>
    /// SkElementPage.xaml 的交互逻辑
    /// </summary>
    public partial class SurfaceSizePage : Window
    {
        public SurfaceSizePage()
        {
            InitializeComponent();
        }


        private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();

            SKPaint paint = new SKPaint
            {
                Color = SKColors.Black,
                TextSize = 40
            };

            float fontSpacing = paint.FontSpacing;
            float x = 20;               // left margin
            float y = fontSpacing;      // first baseline
            float indent = 100;

            canvas.DrawText("SKCanvasView Height and Width:", x, y, paint);
            y += fontSpacing;
            canvas.DrawText(String.Format("{0:F2} x {1:F2}", 
                                          canvasView.Width, canvasView.Height), 
                            x + indent, y, paint);
            y += fontSpacing * 2;
            canvas.DrawText("SKCanvasView CanvasSize:", x, y, paint);
            y += fontSpacing;
            canvas.DrawText(canvasView.CanvasSize.ToString(), x + indent, y, paint);
            y += fontSpacing * 2;
            canvas.DrawText("SKImageInfo Size:", x, y, paint);
            y += fontSpacing;
            canvas.DrawText(info.Size.ToString(), x + indent, y, paint);
		}

    }
}
