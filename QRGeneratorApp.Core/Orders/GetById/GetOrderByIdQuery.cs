using QRGeneratorApp.Core.Common.Mediator;

namespace QRGeneratorApp.Core.Orders.GetById
{
    public record class GetOrderByIdQuery(string Id) : IQuery<GetOrderByIdResult>;
}
