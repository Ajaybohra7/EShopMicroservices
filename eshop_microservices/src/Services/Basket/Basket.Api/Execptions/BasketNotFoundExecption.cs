
namespace Basket.Api.Execptions
{
    public class BasketNotFoundExecption:NotFoundException
    {
        public BasketNotFoundExecption(string userName):base("Basket",userName)
        {
            
        }
    }
}
