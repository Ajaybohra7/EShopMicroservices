using Carter;
using Mapster;
using MediatR;
using Ordering.Application.Dtos;
using Ordering.Application.Orders.Queries.GetOrdersByName;

namespace Ordering.API.Endpoints
{
    public record GetOrderByNameReponse(IEnumerable<OrderDto> orders);
    public class GetOrdersByName : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
           app.MapGet("/orders/{orderName}",async(string name, ISender sender) =>
           {
               var result= await sender.Send(new GetOrdersByNameQuery(name));
               var response= result.Adapt<GetOrderByNameReponse>();
                return Results.Ok(response);
           }).WithDescription("Get Orders by Name")
              .WithTags("Orders")
                .Produces<GetOrderByNameReponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithName("GetOrdersByName");

        }
    }
}
