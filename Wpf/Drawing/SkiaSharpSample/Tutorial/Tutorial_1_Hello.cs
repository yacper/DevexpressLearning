using System;

using SkiaSharp;

namespace SkiaSharpSample.Samples
{
    [Preserve(AllMembers = true)]
    public class Tutorial_1_Hello : SampleBase
    {
        [Preserve]
        public Tutorial_1_Hello()
        {
        }

        public override string Title => "Tutorial_1_Hello";

        public override SampleCategories Category => SampleCategories.Tutoria;

        protected override void OnDrawSample(SKCanvas canvas, int width, int height)
        {
            canvas.DrawColor(SKColors.White);

            using (var paint = new SKPaint())
            {
                paint.TextSize = 64.0f;
                paint.IsAntialias = true;
                paint.Color = (SKColor)0xFF4281A4;
                paint.IsStroke = false;

                canvas.DrawText("Hello World!", width / 2f, 64.0f, paint);
            }

        }
    }
}
