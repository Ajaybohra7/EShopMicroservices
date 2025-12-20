using Carter;
using Mapster;
using MediatR;
using Ordering.Application.Dtos;
using Ordering.Application.Orders.Commands.UpdateOrder;

namespace Ordering.API.Endpoints
{
    public record UpdateOrderRequest(OrderDto Order);
    public record UpdateOrderResponse(bool IsSuccess);
    public class UpdateOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/orders", async (UpdateOrderRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateOrderCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<UpdateOrderResponse>();
                return Results.Ok(response);
            }).WithTags("Orders").WithName("UpdateOrder")
              .WithSummary("Updates an existing order")
              .WithDescription("Updates an existing order in the system with new values provided in the request.")
              .Produces<UpdateOrderResponse>(StatusCodes.Status200OK)
              .ProducesProblem(statusCode: StatusCodes.Status400BadRequest);

        }
    }
}
