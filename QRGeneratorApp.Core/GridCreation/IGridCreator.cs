namespace QRGeneratorApp.Core.GridCreation
{
    public interface IGridCreator
    {
        string CreateGrid(bool[,] qrMap);
    }
}
