

namespace Ordering.Domain.Models
{
    public class OrderItem:Entity<OrderItemId>
    {
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }
        public ProductId ProductId { get; private set; }=default!;
        public OrderId OrderId { get; private set; }=default!;
        internal OrderItem(OrderId orderId, ProductId productId, int quantity, decimal price )
        {
            Id=OrderItemId.Of(Guid.NewGuid()); 
            Price = price;
            Quantity = quantity;
            ProductId = productId;
            OrderId = orderId;

        }

    }
}
