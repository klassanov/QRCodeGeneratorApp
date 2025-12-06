using QRGeneratorApp.Core.Common.Mediator;

namespace QRGeneratorApp.Core.Orders.GetById
{
    public record class GetOrderByIdQuery(Guid Id) : IQuery<GetOrderByIdResult>;
}
