//https://docs.microsoft.com/en-us/dotnet/maui/user-interface/graphics/ 
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Graphics.Skia;
using System;
using System.Windows;
using System.Windows.Threading;
using HorizontalAlignment = Microsoft.Maui.Graphics.HorizontalAlignment;
using VerticalAlignment = Microsoft.Maui.Graphics.VerticalAlignment;

namespace QuickstartWpf
{
    public partial class MainWindow : Window
    {
        readonly DispatcherTimer Timer1 = new();

        public MainWindow()
        {
            InitializeComponent();
            Timer1.Interval = System.TimeSpan.FromMilliseconds(10);
            Timer1.Tick += Timer1_Tick;
        }

        private void SKElement_PaintSurface(object sender, SkiaSharp.Views.Desktop.SKPaintSurfaceEventArgs e)
        {
            ICanvas canvas = new SkiaCanvas() { Canvas = e.Surface.Canvas };

            canvas.FillColor = Colors.White;
            canvas.FillRectangle(0, 0, (float)SkElement1.ActualWidth, (float)SkElement1.ActualHeight);

            canvas.StrokeColor = Colors.Blue.WithAlpha(.5f);
            canvas.StrokeSize = 2;
            for (int i = 0; i < 100; i++)
            {
                float x = Random.Shared.Next((int)SkElement1.ActualWidth);
                float y = Random.Shared.Next((int)SkElement1.ActualHeight);
                float r = Random.Shared.Next(5, 50);
                canvas.DrawCircle(x, y, r);
            }

            canvas.FontColor = Colors.Red;
            canvas.FontSize = 24;

            canvas.Font = Font.Default;
            canvas.DrawString("Text is left aligned.", 20, 20, 380, 100, Microsoft.Maui.Graphics.HorizontalAlignment.Left, Microsoft.Maui.Graphics.VerticalAlignment.Top);
            canvas.DrawString("Text is centered.", 20, 60, 380, 100, Microsoft.Maui.Graphics.HorizontalAlignment.Center, Microsoft.Maui.Graphics.VerticalAlignment.Top);
            canvas.DrawString("Text is right aligned.", 20, 100, 380, 100, Microsoft.Maui.Graphics.HorizontalAlignment.Right, Microsoft.Maui.Graphics.VerticalAlignment.Top);

            canvas.Font = Font.DefaultBold;
            canvas.DrawString("This text is displayed using the bold system font.", 20, 140, 350, 100, Microsoft.Maui.Graphics.HorizontalAlignment.Left, Microsoft.Maui.Graphics.VerticalAlignment.Top);

            canvas.Font = new Font("Arial");
            canvas.FontColor = Colors.Black;
            canvas.SetShadow(new SizeF(6, 6), 4, Colors.Gray);
            canvas.DrawString("This text has a shadow.", 20, 200, 300, 100, Microsoft.Maui.Graphics.HorizontalAlignment.Left, Microsoft.Maui.Graphics.VerticalAlignment.Top);
            canvas.DrawString("hello", 0, 0, Microsoft.Maui.Graphics.HorizontalAlignment.Left);
        }

        private void SKElement_SizeChanged(object sender, SizeChangedEventArgs e) => SkElement1.InvalidateVisual();
        private void Button_Click(object sender, RoutedEventArgs e) => SkElement1.InvalidateVisual();
        private void Timer1_Tick(object? sender, System.EventArgs e) => SkElement1.InvalidateVisual();
        private void Checkbox1_Checked(object sender, RoutedEventArgs e) => Timer1.Start();
        private void Checkbox1_Unchecked(object sender, RoutedEventArgs e) => Timer1.Stop();
    }
}