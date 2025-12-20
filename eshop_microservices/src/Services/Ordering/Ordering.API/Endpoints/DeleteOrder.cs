using Carter;
using Mapster;
using MediatR;
using Ordering.Application.Orders.Commands.DeleteOrder;

namespace Ordering.API.Endpoints
{
    public record DeleteOrderResponse (bool IsSuccess);
    public class DeleteOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/orders/{id}",async(Guid Id, ISender sender) =>
            {
                var result=await sender.Send(new DeleteOrderCommand(Id));
                var response= result.Adapt<DeleteOrderResponse>();
                return Results.Ok(response);

            }).WithDescription("Delete an order by ID")
              .WithTags("Orders")
              .WithName("DeleteOrder")
              .WithSummary("Deletes an existing order given its unique identifier.")
                .Produces<DeleteOrderResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status404NotFound);

        }
    }
}
