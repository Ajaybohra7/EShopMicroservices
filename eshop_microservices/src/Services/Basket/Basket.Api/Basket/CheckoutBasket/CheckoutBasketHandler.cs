
using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Basket.Api.Basket.CheckoutBasket;
public record CheckoutBasketCommand(CheckoutBasketDto BasketCheckoutDto) : ICommand<CheckoutBasketResult>;
public record CheckoutBasketResult(bool Success);

public class CheckoutBasketValidator : AbstractValidator<CheckoutBasketCommand>
{
    public CheckoutBasketValidator()
    {
        RuleFor(x => x.BasketCheckoutDto).NotNull().WithMessage("BasketCheckoutDto").NotEmpty();
        RuleFor(x => x.BasketCheckoutDto.UserName).NotEmpty().WithMessage("Username should not be empty");
      
    }
}

public class CheckoutBasketCommandHandler(IBasketRepository repository,IPublishEndpoint publishEndpoint) : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
{
    public  async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
    {
        var basket=await repository.GetBasket(command.BasketCheckoutDto.UserName,cancellationToken);
        if(basket is null)
        {
           return new CheckoutBasketResult(false);
        }
        var eventMessage=command.BasketCheckoutDto.Adapt<BasketCheckoutEvent>();
        eventMessage.TotalPrice=basket.TotalPrice;
        await publishEndpoint.Publish(eventMessage,cancellationToken);
        await repository.DeleteBasket(basket.UserName,cancellationToken);


    }
}
