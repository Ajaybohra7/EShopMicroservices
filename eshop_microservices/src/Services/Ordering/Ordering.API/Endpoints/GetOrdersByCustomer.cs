using Carter;
using Mapster;
using MediatR;
using Ordering.Application.Dtos;
using Ordering.Application.Orders.Queries.GetOrdersByCutomer;

namespace Ordering.API.Endpoints
{
    public record GetOrderByCustomerResponse(IEnumerable<OrderDto> orders);
    public class GetOrdersByCustomer : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders/{cutomerId}",async (Guid customerId,ISender sender)=>{

                var result= await sender.Send(new GetOrdersByCustomerQuery(customerId));
                var responses=result.Adapt<GetOrderByCustomerResponse>();
                return Results.Ok(responses);
            }).WithDescription("Get Orders By Customer Id").WithTags("Orders")
            .Produces<GetOrderByCustomerResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithName("GetOrdersByCustomer");


        }
    }
}
