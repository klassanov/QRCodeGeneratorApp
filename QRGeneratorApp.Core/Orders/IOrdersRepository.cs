using QRGeneratorApp.Core.Orders.GetById;

namespace QRGeneratorApp.Core.Orders
{
    public interface IOrdersRepository
    {
        Task<GetOrderByIdResult?> GetById(string id);
    }
}
