

using FluentValidation;

namespace Ordering.Application.Orders.Commands.UpdateOrder;
public record UpdateOrderCommand(OrderDto orderDto):ICommand<UpdateOrderResult>;

public record UpdateOrderResult(bool IsSuccess);

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(x => x.orderDto.Id).NotEmpty().WithMessage("Order Id is required");
        RuleFor(x => x.orderDto.CustomerId).NotEmpty().WithMessage("CustomerId is required");
        RuleFor(x => x.orderDto.OrderName).NotEmpty().WithMessage("OrderName is required");
    }
}


