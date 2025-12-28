using BuildingBlocks.Messaging.Events;
using MassTransit;
using Ordering.Application.Orders.Commands.CreateOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Orders.EventHandlers.Integration
{
    public class BasketCheckoutEventHandler(ISender sender,ILogger logger) : IConsumer<BasketCheckoutEvent>
    {
        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);
            // Here you can add the logic to handle the BasketCheckoutEvent, e.g., create an order in the system.
            var command=MapToCreateOrderCommand(context.Message);
            await sender.Send(command);
        }
        private CreateOrderCommand MapToCreateOrderCommand(BasketCheckoutEvent message)
        {
           var  addressDto=new AddressDto(message.FirstName,message.LastName,message.EmailAddress,
            message.AddressLine,message.Country,message.State,message.ZipCode);
            var paymentDto=new PaymentDto(message.CardName,
            message.CardNumber,message.Expiration,message.Cvv, message.PaymentMethod);
            var orderId=Guid.NewGuid();
            var orderDto=new OrderDto
            (
                Id:orderId,
                CustomerId: message.CustomerId,
                OrderName: message.UserName,
                ShippingAddress: addressDto,
                BillingAddress: addressDto,
                Payment: paymentDto,
                Status: Ordering.Domain.Enums.OrderStatus.Pending,
                OrderItems: [
                 new OrderItemDto(orderId, new Guid(),2,500),
                 new OrderItemDto(orderId, new Guid(), 1, 400)
                ]
            );
            return new CreateOrderCommand(orderDto);
        }

    }
}
