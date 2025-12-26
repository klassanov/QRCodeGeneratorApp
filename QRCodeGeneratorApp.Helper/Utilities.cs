namespace QRCodeGeneratorApp.Helper
{
    public static class Utilities
    {
        public static int GenerateRandomNumber(int min = 0, int max = 1)
        {
            return Random.Shared.Next(min, max);
        }
    }
}
