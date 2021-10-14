// https://zhuanlan.zhihu.com/p/365960036
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Brush = System.Drawing.Brush;
using Brushes = System.Drawing.Brushes;
using FontFamily = System.Drawing.FontFamily;

namespace GdiplusWriteableBitmap
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        protected int width;
        protected int height;
        protected WriteableBitmap wBitmap;

        public MainWindow()
        {
            InitializeComponent();


            Loaded += (s, e) =>
            {

                width = (int)OutCanvas.ActualWidth;
                height = (int)OutCanvas.ActualHeight;
                if (width > 0 && height > 0)
                {
                    DisplayImage.Width = width;
                    DisplayImage.Height = height;

                    wBitmap = new WriteableBitmap(width, height, 72, 72, PixelFormats.Bgra32, null);
                    DisplayImage.Source = wBitmap;
                }





            };

        }

        private void Button1_DrawLine(object sender, RoutedEventArgs e)
        {
            wBitmap.Lock();
            Bitmap backBitmap = new Bitmap(width, height, wBitmap.BackBufferStride, System.Drawing.Imaging.PixelFormat.Format32bppArgb, wBitmap.BackBuffer);

            Graphics graphics = Graphics.FromImage(backBitmap);
            graphics.InterpolationMode = InterpolationMode.Bilinear;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.Clear(System.Drawing.Color.White);//整张画布置为白色

            //画一些随机线
            Random rand = new Random();
            for (int i = 0; i < 100; i++)
            {
                int x1 = rand.Next(width);
                int x2 = rand.Next(width);
                int y1 = rand.Next(height);
                int y2 = rand.Next(height);
                graphics.DrawLine(Pens.Red, x1, y1, x2, y2);
            }

            graphics.Flush();
            graphics.Dispose();
            graphics = null;

            backBitmap.Dispose();
            backBitmap = null;

            wBitmap.AddDirtyRect(new Int32Rect(0, 0, width, height));
            wBitmap.Unlock();

        }

        private void Button1_DrawString(object sender, RoutedEventArgs e)
        {
            wBitmap.Lock();
            Bitmap backBitmap = new Bitmap(width, height, wBitmap.BackBufferStride, System.Drawing.Imaging.PixelFormat.Format24bppRgb, wBitmap.BackBuffer);

            Graphics graphics = Graphics.FromImage(backBitmap);
            graphics.InterpolationMode = InterpolationMode.Bilinear;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.Clear(System.Drawing.Color.White);//整张画布置为白色

            FontFamily f = new FontFamily("微软雅黑");            //画一些随机线
            Random rand = new Random();
            for (int i = 0; i < 100; i++)
            {
                int x1 = rand.Next(width);
                int x2 = rand.Next(width);
                int y1 = rand.Next(height);
                int y2 = rand.Next(height);
                //graphics.DrawLine(Pens.Red, x1, y1, x2, y2);
                graphics.DrawString("hello, world", new Font(f, 10), Brushes.Black, x1, y1 );
            }

            graphics.Flush();
            graphics.Dispose();
            graphics = null;

            backBitmap.Dispose();
            backBitmap = null;

            wBitmap.AddDirtyRect(new Int32Rect(0, 0, width, height));
            wBitmap.Unlock();

        }



        private void Button2_RawPixcel(object sender, RoutedEventArgs e)
        {
            unsafe
            {
                var bytes = (byte*)wBitmap.BackBuffer.ToPointer();
                wBitmap.Lock();

                //整张画布置为白色
                for (int i = wBitmap.BackBufferStride * wBitmap.PixelHeight - 1; i >= 0; i--)
                {
                    bytes[i] = 255;
                }

                //画一些随机的红点
                Random rand = new Random();
                for (int i = 0; i < 10000; i++)
                {
                    int x = rand.Next(width);
                    int y = rand.Next(height);
                    int array_start = y * wBitmap.BackBufferStride + x * 3;

                    bytes[array_start] = 0;
                    bytes[array_start + 1] = 0;
                    bytes[array_start + 2] = 255;
                }

                wBitmap.AddDirtyRect(new Int32Rect(0, 0, width, height));
                wBitmap.Unlock();
            }

        }

        private void Button1_DrawRect(object sender, RoutedEventArgs e)
        {
            wBitmap.Lock();
            Bitmap backBitmap = new Bitmap(width, height, wBitmap.BackBufferStride, System.Drawing.Imaging.PixelFormat.Format24bppRgb, wBitmap.BackBuffer);

            Graphics graphics = Graphics.FromImage(backBitmap);
            graphics.InterpolationMode = InterpolationMode.Bilinear;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.Clear(System.Drawing.Color.White);//整张画布置为白色

            //画一些随机线
            Random rand = new Random();
            for (int i = 0; i < 100; i++)
            {
                int x1 = rand.Next(width);
                int x2 = rand.Next(width);
                int y1 = rand.Next(height);
                int y2 = rand.Next(height);
                graphics.DrawRectangle(Pens.Red, x1, y1, x2, y2);
                if(i > 80)
                    graphics.FillRectangle(Brushes.Red, x1, y1, x2, y2);
            }

            graphics.Flush();
            graphics.Dispose();
            graphics = null;

            backBitmap.Dispose();
            backBitmap = null;

            wBitmap.AddDirtyRect(new Int32Rect(0, 0, width, height));
            wBitmap.Unlock();

        }
    }
}
