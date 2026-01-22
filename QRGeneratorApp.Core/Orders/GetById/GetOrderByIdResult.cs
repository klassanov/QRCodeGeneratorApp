namespace QRGeneratorApp.Core.Orders.GetById
{
    public record class GetOrderByIdResult(
        string Id,
        string CustomerName,
        string Text,
        DateTime CreatedAt
    );
}
