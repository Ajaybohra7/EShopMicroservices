using BuildingBlocks.Exceptions;

namespace CatalogAPI.Execptions
{
    public class ProductNotFoundException:NotFoundException
    {
        public ProductNotFoundException(Guid Id ):base("Product",Id)
        {
        }
    }
}
