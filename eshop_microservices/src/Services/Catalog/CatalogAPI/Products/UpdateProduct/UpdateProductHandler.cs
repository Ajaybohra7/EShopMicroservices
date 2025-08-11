
namespace CatalogAPI.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price) : ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool IsSuccess);

    public class UpdateProductCommandHandler(IDocumentSession session, ILogger<UpdateProductCommandHandler> logger)
            : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("UpdateProductHandler. Handle called with {@Command}", command);
            var Product = await session.LoadAsync<Product>(command.Id, cancellationToken);
            if (Product == null)
            {
                logger.LogWarning("Product with Id {ProductId} not found", command.Id);
                throw new ProductNotFoundException();
            }
            Product.Name = command.Name;
            Product.Category = command.Category;
            Product.Description = command.Description;
            Product.ImageFile = command.ImageFile;
            Product.Price = command.Price;
            session.Update(Product);
            await session.SaveChangesAsync(cancellationToken);
            return new UpdateProductResult(true);
        }
    }
}
