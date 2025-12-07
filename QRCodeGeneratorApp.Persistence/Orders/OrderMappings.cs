using System;
using System.Collections.Generic;
using System.Text;
using QRGeneratorApp.Core.Orders.GetById;

namespace QRCodeGeneratorApp.Persistence.Orders
{
    public static class OrderMappings
    {
        public static GetOrderByIdResult ToGetOrderByIdResult(this OrderDocument orderDocument) =>
            new(
                orderDocument.Id.ToString(),
                orderDocument.CustomerName!,
                orderDocument.Text!,
                orderDocument.CreatedAt
            );
    }
}
