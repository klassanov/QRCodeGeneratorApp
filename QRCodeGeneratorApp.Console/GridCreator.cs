using SkiaSharp;

namespace QRCodeGeneratorApp.Console
{
    internal static class GridCreator
    {
        internal static void CreateGrid(bool[,] qrMap, int length)
        {

            int N = length;                          // grid dimension (NxN)
            int cellSize = 50;                   // size of each square cell in pixels
                                                 // FillMode mode = FillMode.Checkerboard; // or FillMode.Random
            int paddingNumber = 40;              // space reserved for numbers on each side
            string outFile = "grid_skia.png";
            //var rand = new Random(0);            // deterministic; change seed if you like

            // --- compute canvas size ---
            int topMargin = paddingNumber;
            int bottomMargin = paddingNumber;
            int leftMargin = paddingNumber;
            int rightMargin = paddingNumber;

            int width = leftMargin + (N * cellSize) + rightMargin;
            int height = topMargin + (N * cellSize) + bottomMargin;

            using var surface = SKSurface.Create(new SKImageInfo(width, height));
            SKCanvas canvas = surface.Canvas;

            // white background
            canvas.Clear(SKColors.White);

            // draw cells
            for (int r = 0; r < N; r++)
            {
                for (int c = 0; c < N; c++)
                {
                    SKColor fillColor = qrMap[r, c] ? SKColors.Black : SKColors.White;

                    int x = leftMargin + c * cellSize;
                    int y = topMargin + r * cellSize;
                    var rect = new SKRect(x, y, x + cellSize, y + cellSize);
                    using var paint = new SKPaint
                    {
                        Color = fillColor,
                        Style = SKPaintStyle.Fill
                    };
                    canvas.DrawRect(rect, paint);
                }
            }

            // draw grid lines
            using (var pen = new SKPaint
            {
                Color = SKColors.Gray,
                StrokeWidth = 1,
                IsStroke = true
            })
            {
                for (int i = 0; i <= N; i++)
                {
                    int x = leftMargin + i * cellSize;
                    canvas.DrawLine(x, topMargin, x, topMargin + N * cellSize, pen);
                }
                for (int i = 0; i <= N; i++)
                {
                    int y = topMargin + i * cellSize;
                    canvas.DrawLine(leftMargin, y, leftMargin + N * cellSize, y, pen);
                }
            }

            // text paint
            using var textPaint = new SKPaint
            {
                Color = SKColors.Black,
                IsAntialias = true,
            };
            using var textFont = new SKFont
            {
                Size = 20 // Set desired font size here
            };

            // column numbers (top & bottom)
            for (int c = 0; c < N; c++)
            {
                string label = (c + 1).ToString();
                float centerX = leftMargin + c * cellSize + cellSize / 2f;

                // top
                canvas.DrawText(label, centerX, topMargin / 2f + textFont.Size / 2f, SKTextAlign.Center, textFont, textPaint);

                // bottom
                canvas.DrawText(label, centerX, topMargin + N * cellSize + bottomMargin / 2f + textFont.Size / 2f, SKTextAlign.Center, textFont, textPaint);
            }

            // row numbers (left & right)
            for (int r = 0; r < N; r++)
            {
                string label = (r + 1).ToString();
                float centerY = topMargin + r * cellSize + cellSize / 2f + textFont.Size / 2f;

                // left
                canvas.DrawText(label, leftMargin / 2f, centerY, SKTextAlign.Center, textFont, textPaint);

                // right
                canvas.DrawText(label, leftMargin + N * cellSize + rightMargin / 2f, centerY, SKTextAlign.Center, textFont, textPaint);
            }

            // save to file
            using var image = surface.Snapshot();
            using var data = image.Encode(SKEncodedImageFormat.Png, 100);
            using var stream = File.OpenWrite(outFile);
            data.SaveTo(stream);

        }
    }
}
