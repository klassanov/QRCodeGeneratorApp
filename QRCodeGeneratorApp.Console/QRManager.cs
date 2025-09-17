using Net.Codecrete.QrCodeGenerator;

namespace QRCodeGeneratorApp.Console
{
    internal class QRManager
    {
        internal static bool[,] GenerateQRMap(string text)
        {
            // Create the QR code symbol
            var qr = QrCode.EncodeText(text, QrCode.Ecc.Low);
            
            //qr.SaveAsPng(filename, scale: 10, border: 4);

            bool[,] qrMap = new bool[qr.Size,qr.Size];

            for (int j = 0; j < qr.Size; j++)
            {
                for (int i = 0; i < qr.Size; i++)
                {
                    qrMap[i, j] = qr.GetModule(i, j); // true for black, false for white
                }
            }

            return qrMap;
        }

    }
}