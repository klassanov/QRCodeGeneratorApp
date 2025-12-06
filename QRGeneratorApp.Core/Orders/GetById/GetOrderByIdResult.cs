namespace QRGeneratorApp.Core.Orders.GetById
{
    public record class GetOrderByIdResult(
        Guid Id,
        string ClientName,
        string RequestedText,
        DateTime RequestTime
    );
}
