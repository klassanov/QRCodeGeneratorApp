namespace QRGeneratorApp.Core.QRMapCreation
{
    public interface IQRMapCreator
    {
        bool[,] GenerateQRMap(string text);
    }
}