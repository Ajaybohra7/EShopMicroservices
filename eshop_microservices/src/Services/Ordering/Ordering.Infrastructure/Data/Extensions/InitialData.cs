


namespace Ordering.Infrastructure.Data.Extensions
{
    internal class InitialData
    {
        public static IEnumerable<Customer> Customers=> new List<Customer>
            {
            Customer.Create(CustomerId.Of(new Guid("d290f1ee-6c54-4b01-90e6-d701748f0851")),"John Doe","john@gmail.com"),
            Customer.Create(CustomerId.Of(new Guid("c290f1ee-6c54-4b01-90e6-d701748f0852")),"Jane Smith","jane@gmail.com    ")
            };

        public static IEnumerable<Product> Products => new List<Product>
            {
            Product.Create(ProductId.Of(new Guid("e290f1ee-6c54-4b01-90e6-d701748f0853")),"Laptop",999),
            Product.Create(ProductId.Of(new Guid("f290f1ee-6c54-4b01-90e6-d701748f0854")),"Smartphone",499),
            Product.Create(ProductId.Of(new Guid("f390f1ee-6c54-4b01-90e6-d701748f0854")),"Tablet",299),
            Product.Create(ProductId.Of(new Guid("f490f1ee-6c54-4b01-90e6-d701748f0854")),"Monitor",199)
            };
      
        public static IEnumerable<Order> OrdersWithItems
        {
            get
            {
                var address1 = Address.Of("mehmet", "dimh", "mehmet@gmail.com", "reajrnei", "delhi", "city", "110022");
                var address2 = Address.Of("john", "doe", "john@gmai.com","dfewfe", "newyork", "city", "220011");

                var payment1 = Payment.Of(1, "4111111111111111", "mehmet" , "12 /25", "123");
                var payment2 = Payment.Of(2, "5500000000000004", "john", "11 /24", "456");

                var order1 = Order.Create(
                    OrderId.Of(Guid.NewGuid()),
                    CustomerId.Of(new Guid("d290f1ee-6c54-4b01-90e6-d701748f0851")), 
                    OrderName.Of("Ord_1"),
                    shippingAddress: address1, 
                    billingAddress: address1,
                    payment1
                    );
                order1.Add(ProductId.Of(new Guid("e290f1ee-6c54-4b01-90e6-d701748f0853")), 2, 500);
                order1.Add(ProductId.Of(new Guid("f290f1ee-6c54-4b01-90e6-d701748f0854")), 1, 300);

                var order2 = Order.Create(
                    OrderId.Of(Guid.NewGuid()),
                    CustomerId.Of(new Guid("c290f1ee-6c54-4b01-90e6-d701748f0852")), 
                    OrderName.Of("ORD_2"),
                    shippingAddress: address2, 
                    billingAddress: address2,
                    payment2
                    );

                order2.Add(ProductId.Of(new Guid("f390f1ee-6c54-4b01-90e6-d701748f0854")), 1, 700);
                order2.Add(ProductId.Of(new Guid("f490f1ee-6c54-4b01-90e6-d701748f0854")), 3, 150);

                return new  List<Order> { order1, order2  };


            }
        }
    }
}
