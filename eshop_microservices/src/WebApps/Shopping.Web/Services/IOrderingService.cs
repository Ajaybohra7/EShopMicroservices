using Shopping.Web.Models.Ordering;

namespace Shopping.Web.Services
{
    public interface IOrderingService
    {
        [Get("/ordering-service/orders?pageIndex={pageIndex}&pageSize={pageSize}\"")]
        Task<GetOrdersResponse> GetOrders(int? pageIndex = 1, int? pageSize = 10);

        [Get("/ordering-service/orders/{orderName}")]

       Task<GetOrderByNameReponse> GetOrdersByName(string orderName);


        [Get("/ordering-service/orders/customer/{customerId}")]
        Task<GetOrderByCustomerResponse> GetOrdersByCustomer(Guid customerId);
    }
}
