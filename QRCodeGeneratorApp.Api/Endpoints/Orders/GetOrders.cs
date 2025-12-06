using Carter;
using Microsoft.AspNetCore.Mvc;
using QRGeneratorApp.Core.Common.Mediator;
using QRGeneratorApp.Core.Orders.GetById;

namespace QRCodeGeneratorApp.Api.Endpoints.ClientRequests
{
    public class GetOrders : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders/{id:guid}", async (Guid id, [FromServices] IQueryHandler<GetOrderByIdQuery, GetOrderByIdResult> queryHandler) =>
            {
                var query = new GetOrderByIdQuery(id);
                var result = await queryHandler.Handle(query);
                return Results.Ok(result);
            });
        }
    }
}
