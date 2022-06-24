//https://docs.microsoft.com/en-us/dotnet/maui/user-interface/graphics/

using Microsoft.Maui.Graphics;
using Microsoft.Maui.Graphics.Platform;
using Microsoft.Maui.Graphics.Skia;
using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using Color = Microsoft.Maui.Graphics.Color;
using Colors = Microsoft.Maui.Graphics.Colors;
using Point = Microsoft.Maui.Graphics.Point;
using Rect = Microsoft.Maui.Graphics.Rect;

namespace QuickstartWpf;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        Timer1.Interval =  System.TimeSpan.FromMilliseconds(10);
        Timer1.Tick     += Timer1_Tick;
    }

    private void SKElement_PaintSurface(object sender, SkiaSharp.Views.Desktop.SKPaintSurfaceEventArgs e)
    {
        ICanvas canvas = new SkiaCanvas() { Canvas = e.Surface.Canvas };

        canvas.FillColor = Colors.White;
        canvas.FillRectangle(0, 0, (float)SkElement1.ActualWidth, (float)SkElement1.ActualHeight);

        canvas.StrokeColor = Colors.Blue.WithAlpha(.5f);
        canvas.StrokeSize  = 2;
        for (int i = 0; i < 100; i++)
        {
            float x = Random.Shared.Next((int)SkElement1.ActualWidth);
            float y = Random.Shared.Next((int)SkElement1.ActualHeight);
            float r = Random.Shared.Next(5, 50);
            canvas.DrawCircle(x, y, r);
        }

        canvas.FontColor = Colors.Red;
        canvas.FontSize  = 24;

        canvas.DrawRectangle(100, 100, 10, 20);

        canvas.Font = Font.Default;
        canvas.DrawString("Text is left aligned.", 20, 20, 380, 100, Microsoft.Maui.Graphics.HorizontalAlignment.Left, Microsoft.Maui.Graphics.VerticalAlignment.Top);
        canvas.DrawString("Text is centered.", 20, 60, 380, 100, Microsoft.Maui.Graphics.HorizontalAlignment.Center, Microsoft.Maui.Graphics.VerticalAlignment.Top);
        canvas.DrawString("Text is right aligned.", 20, 100, 380, 100, Microsoft.Maui.Graphics.HorizontalAlignment.Right, Microsoft.Maui.Graphics.VerticalAlignment.Top);

        canvas.Font = Font.DefaultBold;
        canvas.DrawString("This text is displayed using the bold system font.", 20, 140, 350, 100, Microsoft.Maui.Graphics.HorizontalAlignment.Left, Microsoft.Maui.Graphics.VerticalAlignment.Top);

        canvas.Font      = new Font("Arial");
        canvas.FontColor = Colors.Black;
        canvas.SetShadow(new SizeF(6, 6), 4, Colors.Gray);
        canvas.DrawString("This text has a shadow.", 20, 200, 300, 100, Microsoft.Maui.Graphics.HorizontalAlignment.Left, Microsoft.Maui.Graphics.VerticalAlignment.Top);
        canvas.DrawString("hello", 0, 0, Microsoft.Maui.Graphics.HorizontalAlignment.Left);

        System.Diagnostics.Debug.WriteLine("ok");

        CreateBmp();

        DrawImage(canvas);


        LinearGradientPaint linearGradientPaint = new LinearGradientPaint
        {
            StartColor = Colors.Yellow,
            EndColor = Colors.Green,
            // StartPoint is already (0,0)
            EndPoint = new Point(0, 1)

        };

        RectF linearRectangle = new RectF(10, 10, 200, 100);
        canvas.SetFillPaint(linearGradientPaint, linearRectangle);
        canvas.SetShadow(new SizeF(10, 10), 10, Colors.Grey);
        canvas.FillRoundedRectangle(linearRectangle, 12);
    }

    private void SKElement_SizeChanged(object sender, SizeChangedEventArgs e) => SkElement1.InvalidateVisual();

    private void Button_Click(object sender, RoutedEventArgs e) => SkElement1.InvalidateVisual();

    private void Timer1_Tick(object? sender, System.EventArgs e) => SkElement1.InvalidateVisual();

    private void Checkbox1_Checked(object sender, RoutedEventArgs e) => Timer1.Start();

    private void Checkbox1_Unchecked(object sender, RoutedEventArgs e) => Timer1.Stop();

    private readonly DispatcherTimer Timer1 = new();

    void DrawImage(ICanvas canvas)
    {
        Assembly assembly = GetType().GetTypeInfo().Assembly;

        IImage image;
        using (Stream stream = assembly.GetManifestResourceStream("QuickstartWpf.Fist.png"))
            //using (Stream stream = assembly.GetManifestResourceStream("QuickstartWpf.Resources.Images.console2.png"))
        {
            image = SkiaImage.FromStream(stream, ImageFormat.Png);
        }

        if (image != null) { canvas.DrawImage(image, 10, 10, image.Width, image.Height); }
    }


    void CreateBmp()
    {
        // Create a bitmap in memory and draw on its Canvas
        SkiaBitmapExportContext bmp    = new(600, 400, 1.0f);
        ICanvas                 canvas = bmp.Canvas;

        // Draw a big blue rectangle with a dark border
        Rect backgroundRectangle = new(0, 0, bmp.Width, bmp.Height);
        canvas.FillColor = Color.FromArgb("#003366");
        canvas.FillRectangle(backgroundRectangle);
        canvas.StrokeColor = Colors.Black;
        canvas.StrokeSize  = 20;
        canvas.DrawRectangle(backgroundRectangle);

        // Draw circles randomly around the image
        for (int i = 0; i < 100; i++)
        {
            float x = Random.Shared.Next(bmp.Width);
            float y = Random.Shared.Next(bmp.Height);
            float r = Random.Shared.Next(5, 50);

            Color randomColor = Color.FromRgb(
                                              red: Random.Shared.Next(255),
                                              green: Random.Shared.Next(255),
                                              blue: Random.Shared.Next(255));

            canvas.StrokeSize  = r / 3;
            canvas.StrokeColor = randomColor.WithAlpha(.3f);
            canvas.DrawCircle(x, y, r);
        }

        // Measure a string
        string myText     = "Hello, Maui.Graphics!";
        Font   myFont     = new Font("Impact");
        float  myFontSize = 48;
        canvas.Font = myFont;
        SizeF textSize = canvas.GetStringSize(myText, myFont, myFontSize);

        // Draw a rectangle to hold the string
        Point point = new(
                          x: (bmp.Width - textSize.Width) / 2,
                          y: (bmp.Height - textSize.Height) / 2);
        Rect myTextRectangle = new(point, textSize);
        canvas.FillColor = Colors.Black.WithAlpha(.5f);
        canvas.FillRectangle(myTextRectangle);
        canvas.StrokeSize  = 2;
        canvas.StrokeColor = Colors.Yellow;
        canvas.DrawRectangle(myTextRectangle);

        // Daw the string itself
        canvas.FontSize  = myFontSize * .9f; // smaller than the rectangle
        canvas.FontColor = Colors.White;
        canvas.DrawString(myText, myTextRectangle,
                          Microsoft.Maui.Graphics.HorizontalAlignment.Center, Microsoft.Maui.Graphics.VerticalAlignment.Center, TextFlow.OverflowBounds);

        // Save the image as a PNG file
        bmp.WriteToFile("console2.png");
    }
}