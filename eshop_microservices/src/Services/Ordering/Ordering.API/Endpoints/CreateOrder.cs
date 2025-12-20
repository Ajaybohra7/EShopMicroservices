using Carter;
using Mapster;
using MediatR;
using Ordering.Application.Dtos;
using Ordering.Application.Orders.Commands.CreateOrder;

namespace Ordering.API.Endpoints
{
   public record CreateOrderRequet(OrderDto Order);
    public record CreateOrderResponse(Guid Id);

    public class CreateOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/orders", async (CreateOrderRequet request, ISender sender) =>
            {
                var command = request.Adapt<CreateOrderCommand>();
                var result = await sender.Send(command);

                var respone = result.Adapt<CreateOrderResponse>();

                return Results.Created($"/orders/{respone.Id}", respone);

            }).WithDescription("Create a new order")
              .WithName("CreateOrder")
              .Produces<CreateOrderResponse>(StatusCodes.Status201Created)
              .Produces(StatusCodes.Status400BadRequest)
              .WithSummary("Creates a new order in the system."); 

        }
    }
}
