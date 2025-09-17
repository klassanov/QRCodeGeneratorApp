using SkiaSharp;

namespace QRGeneratorApp.Core.GridCreation
{
    internal class GridCreator: IGridCreator
    {
        private readonly GridConfig config;

        public GridCreator(GridConfig config)
        {
            this.config = config;
        }

        public string CreateGrid(bool[,] qrMap)
        {
            int N = qrMap.GetLength(0);          // grid dimension (NxN)
            int cellSizePx = config.CellSizePx;                 // size of each square cell in pixels
            int paddingNumber = config.PaddingNumber;              // space reserved for numbers on each side
            string outFile = "grid_skia.png";

            // --- compute canvas size ---
            int topMargin = paddingNumber;
            int bottomMargin = paddingNumber;
            int leftMargin = paddingNumber;
            int rightMargin = paddingNumber;

            int width = leftMargin + N * cellSizePx + rightMargin;
            int height = topMargin + N * cellSizePx + bottomMargin;

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

                    int x = leftMargin + c * cellSizePx;
                    int y = topMargin + r * cellSizePx;
                    var rect = new SKRect(x, y, x + cellSizePx, y + cellSizePx);
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
                StrokeWidth = config.StrokeThrough,
                IsStroke = true
            })
            {
                for (int i = 0; i <= N; i++)
                {
                    int x = leftMargin + i * cellSizePx;
                    canvas.DrawLine(x, topMargin, x, topMargin + N * cellSizePx, pen);
                }
                for (int i = 0; i <= N; i++)
                {
                    int y = topMargin + i * cellSizePx;
                    canvas.DrawLine(leftMargin, y, leftMargin + N * cellSizePx, y, pen);
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
                Size = config.FontSize // Set desired font size here
            };

            // column numbers (top & bottom)
            for (int c = 0; c < N; c++)
            {
                string label = (c + 1).ToString();
                float centerX = leftMargin + c * cellSizePx + cellSizePx / 2f;

                // top
                canvas.DrawText(label, centerX, topMargin / 2f + textFont.Size / 2f, SKTextAlign.Center, textFont, textPaint);

                // bottom
                canvas.DrawText(label, centerX, topMargin + N * cellSizePx + bottomMargin / 2f + textFont.Size / 2f, SKTextAlign.Center, textFont, textPaint);
            }

            // row numbers (left & right)
            for (int r = 0; r < N; r++)
            {
                string label = (r + 1).ToString();
                float centerY = topMargin + r * cellSizePx + cellSizePx / 2f + textFont.Size / 2f;

                // left
                canvas.DrawText(label, leftMargin / 2f, centerY, SKTextAlign.Center, textFont, textPaint);

                // right
                canvas.DrawText(label, leftMargin + N * cellSizePx + rightMargin / 2f, centerY, SKTextAlign.Center, textFont, textPaint);
            }

            // save to file
            using var image = surface.Snapshot();
            using var data = image.Encode(SKEncodedImageFormat.Png, 100);
            using var stream = File.OpenWrite(outFile);
            data.SaveTo(stream);

            return outFile;

        }
    }
}
