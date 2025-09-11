
namespace CatalogAPI.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price) : ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool IsSuccess);
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Product Id is required.");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Product Name is required.").Length(2,150).WithMessage("Name must be in between 2 and 150 characters.");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Product price should be greater than 0.");
        }
    }

    public class UpdateProductCommandHandler(IDocumentSession session)
            : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var Product = await session.LoadAsync<Product>(command.Id, cancellationToken);
            if (Product == null)
            {
                throw new ProductNotFoundException(command.Id);
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
