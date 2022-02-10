using System;

using SkiaSharp;

namespace SkiaSharpSample.Samples
{
    [Preserve(AllMembers = true)]
    public class Tutorial_2_Coordinate : SampleBase
    {
        [Preserve]
        public Tutorial_2_Coordinate()
        {
        }

        public override string Title => "Tutorial_2_Coordinate";

        public override SampleCategories Category => SampleCategories.Tutoria;

        protected override void OnDrawSample(SKCanvas canvas, int width, int height)
        {
            canvas.DrawColor(SKColors.White);

            using (var paint = new SKPaint())
            {
                paint.TextSize = 32.0f;
                paint.IsAntialias = true;
                paint.Color = (SKColor)0xFF4281A4;
                paint.IsStroke = false;

                canvas.DrawText("(0,0)", 0, 0, paint);

            }
            using (var paint = new SKPaint())
            {
                paint.TextSize = 32.0f;
                paint.IsAntialias = true;
                paint.Color = (SKColor)0xFF4281A4;
                paint.IsStroke = false;

                canvas.DrawText("(400,0)", 400, 0, paint);
            }
            using (var paint = new SKPaint())
            {
                paint.TextSize = 32.0f;
                paint.IsAntialias = true;
                paint.Color = (SKColor)0xFF4284A4;
                paint.IsStroke = false;

                canvas.DrawText("(0,400)", 0, 400, paint);
            }

            using (var paint = new SKPaint())
            {
                paint.TextSize = 32.0f;
                paint.IsAntialias = true;
                paint.Color = (SKColor)0xFF4284A4;
                paint.IsStroke = false;

                canvas.DrawText("(400,400)", 400, 400, paint);
            }




        }
    }
}
