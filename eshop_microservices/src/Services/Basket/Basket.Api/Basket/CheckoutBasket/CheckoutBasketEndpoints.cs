


namespace Basket.Api.Basket.CheckoutBasket;

public record CheckoutBasketRequest(CheckoutBasketDto BasketCheckoutDto);
public record CheckoutBasketResponse(bool IsSuccess);
public class CheckoutBasketEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket/checkout",async(CheckoutBasketRequest request, ISender sender) =>
        {
           var command=request.Adapt<CheckoutBasketCommand>();
           var result= await sender.Send(command);
           var response=result.Adapt<CheckoutBasketResponse>();
            return Results.Ok(response);
        })
        .WithName("CheckoutBasket")
        .Produces(StatusCodes.Status202Accepted)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Checkout Basket")
        .WithDescription("Checkout Basket for order processing");

    }
}
