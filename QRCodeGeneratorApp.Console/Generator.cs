using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Net.Codecrete.QrCodeGenerator;

namespace QRCodeGeneratorApp.Console
{
    internal class Generator
    {
        internal static void Generate()
        {
            var text = "Бонииии!";
            var filename = "hello-world-QR.png";

            //char whiteSquare = '⬜'; // Unicode U+2B1C
            //char blackSquare = '⬛'; // Unicode U+2B1B

            //string whiteSquare = "\u2B1C"; // ⬜
            //string blackSquare = "\u2B1B"; // ⬛

            //var qr = QrCode.EncodeText(text, QrCode.Ecc.Medium); // Create the QR code symbol
            var qr = QrCode.EncodeText(text, QrCode.Ecc.Low); // Create the QR code symbol
            qr.SaveAsPng(filename, scale: 10, border: 4);

            bool[,] qrMap = new bool[qr.Size,qr.Size];

            for (int y = 0; y < qr.Size; y++)
            {
                for (int x = 0; x < qr.Size; x++)
                {
                    qrMap[x, y] = qr.GetModule(x, y); // true for black, false for white
                }
            }

            //DrawGrid(qrMap, qr.Size);

            GridCreator.CreateGrid(qrMap, qr.Size);


            //Console.WriteLine($"The QR code has been saved as {Path.GetFullPath(filename)}");

            //System.Console.OutputEncoding = System.Text.Encoding.UTF8;

            //for (int x = 0; x < qr.Size; x++)
            //{
            //    for (int y = 0; y < qr.Size; y++)
            //    {
            //        System.Console.Write(qrMap[x, y]);
            //    }
            //    System.Console.WriteLine();
            //}

            //System.Console.WriteLine();
            //System.Console.WriteLine("Print version");
            //System.Console.WriteLine();

            //for (int y = 0; y < qr.Size; y++)
            //{
            //    System.Console.Write((y + 1).ToString());

            //    for (int x = 0; x < qr.Size; x++)
            //    {
            //        System.Console.Write(qr.GetModule(x, y) // true for black, false for white
            //            ? blackSquare
            //            : whiteSquare
            //        );
            //    }

            //    System.Console.Write((y + 1).ToString());
            //    System.Console.WriteLine();

            //for (int i = 0; i < qr.Size; i++)
            //{
            //    System.Console.Write("-");
            //}
            //System.Console.WriteLine();
            //}


        }

        //enum FillMode { Checkerboard, Random };

        internal static void DrawGrid(bool[,] qrMap, int length)
        {
            // --- configuration ---
            int N = length;                       // grid dimension (NxN)
            int cellSize = 50;                // size of each square cell in pixels
            //FillMode mode = FillMode.Checkerboard; // or FillMode.Random
            int paddingNumber = 40;           // space (pixels) reserved for numbers on each side
            int lineThickness = 1;
            string outFile = "grid.png";
            Font labelFont = new Font("Arial", 14, FontStyle.Regular, GraphicsUnit.Pixel);

            // --- compute canvas size ---
            int topMargin = paddingNumber;
            int bottomMargin = paddingNumber;
            int leftMargin = paddingNumber;
            int rightMargin = paddingNumber;

            int width = leftMargin + (N * cellSize) + rightMargin;
            int height = topMargin + (N * cellSize) + bottomMargin;

            var bmp = new Bitmap(width, height);
            using (var g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

                // draw grid cells
                //var rand = new Random(0); // deterministic; change seed for different randoms
                for (int r = 0; r < N; r++)
                {
                    for (int c = 0; c < N; c++)
                    {
                        // choose color
                        Color fillColor = qrMap[r,c] ? Color.Black : Color.White;
                        //if (mode == FillMode.Checkerboard)
                        //{
                        //    // classic checkerboard
                        //    fillColor = ((r + c) % 2 == 0) ? Color.White : Color.Black;
                        //}
                        //else // Random
                        //{
                        //    fillColor = (rand.Next(2) == 0) ? Color.White : Color.Black;
                        //}

                        int x = leftMargin + c * cellSize;
                        int y = topMargin + r * cellSize;
                        var rect = new Rectangle(x, y, cellSize, cellSize);
                        using (var brush = new SolidBrush(fillColor))
                        {
                            g.FillRectangle(brush, rect);
                        }
                    }
                }

                // draw grid lines
                using (var pen = new Pen(Color.Gray, lineThickness))
                {
                    // vertical lines
                    for (int i = 0; i <= N; i++)
                    {
                        int x = leftMargin + i * cellSize;
                        g.DrawLine(pen, x, topMargin, x, topMargin + N * cellSize);
                    }

                    // horizontal lines
                    for (int i = 0; i <= N; i++)
                    {
                        int y = topMargin + i * cellSize;
                        g.DrawLine(pen, leftMargin, y, leftMargin + N * cellSize, y);
                    }
                }

                // draw numbers for columns (top and bottom)
                StringFormat sfCenter = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                for (int c = 0; c < N; c++)
                {
                    string label = (c + 1).ToString();
                    int centerX = leftMargin + c * cellSize + cellSize / 2;

                    // top
                    Rectangle topRect = new Rectangle(centerX - cellSize / 2, 0, cellSize, topMargin);
                    g.DrawString(label, labelFont, Brushes.Black, topRect, sfCenter);

                    // bottom
                    Rectangle bottomRect = new Rectangle(centerX - cellSize / 2, topMargin + N * cellSize, cellSize, bottomMargin);
                    g.DrawString(label, labelFont, Brushes.Black, bottomRect, sfCenter);
                }

                // draw numbers for rows (left and right)
                for (int r = 0; r < N; r++)
                {
                    string label = (r + 1).ToString();
                    int centerY = topMargin + r * cellSize + cellSize / 2;

                    // left
                    Rectangle leftRect = new Rectangle(0, centerY - cellSize / 2, leftMargin, cellSize);
                    g.DrawString(label, labelFont, Brushes.Black, leftRect, sfCenter);

                    // right
                    Rectangle rightRect = new Rectangle(leftMargin + N * cellSize, centerY - cellSize / 2, rightMargin, cellSize);
                    g.DrawString(label, labelFont, Brushes.Black, rightRect, sfCenter);
                }
            }

            // save file
            bmp.Save(outFile, ImageFormat.Png);
            bmp.Dispose();

            System.Console.WriteLine($"Saved {outFile} ({N}x{N} grid).");
            System.Console.WriteLine("Done.");
        }

    }
}