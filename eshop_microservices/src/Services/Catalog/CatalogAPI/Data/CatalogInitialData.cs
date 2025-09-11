using Marten.Schema;

namespace CatalogAPI.Data
{
    public class CatalogInitialData : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            using var session = store.LightweightSession();
            if (await session.Query<Product>().AnyAsync(cancellation))
            {
                return;
            }

            session.Store<Product>(GetConfiguredProduct);
            await session.SaveChangesAsync(cancellation);
        }
        private static IEnumerable<Product> GetConfiguredProduct => new List<Product>()
        {
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "IPhone 13 Pro",
                Category = new List<string> { "Smart Phone" },
                Description = "IPhone 13 Pro is the latest smartphone from Apple with advanced features and improved performance.",
                ImageFile = "product-1.png",
                Price = 999.99M
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Samsung Galaxy S21",
                Category = new List<string> { "Smart Phone" },
                Description = "Samsung Galaxy S21 is a high-end smartphone with a sleek design and powerful capabilities.",
                ImageFile = "product-2.png",
                Price = 899.99M
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Google Pixel 6",
                Category = new List<string> { "Smart Phone" },
                Description = "Google Pixel 6 offers a pure Android experience with excellent camera quality and performance.",
                ImageFile = "product-3.png",
                Price = 799.99M
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "OnePlus 9 Pro",
                Category = new List<string> { "Smart Phone" },
                Description = "OnePlus 9 Pro is a flagship smartphone known for its fast performance and smooth user experience.",
                ImageFile = "product-4.png",
                Price = 969.99M
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Sony WH-1000XM4",
                Category = new List<string> { "Headphones" },
                Description = "Sony WH-1000XM4 are premium noise-canceling headphones with exceptional sound quality and comfort.",
                ImageFile = "product-5.png",
                Price = 349.99M
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Bose QuietComfort 35 II",
                Category = new List<string> { "Headphones" },
                Description = "Bose QuietComfort 35 II are wireless headphones that provide excellent noise cancellation and audio performance.",
                ImageFile = "product-6.png",
                Price = 299.99M
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Apple AirPods Pro",
                Category = new List<string> { "Headphones" },
                Description = "Apple AirPods Pro are true wireless earbuds with active noise cancellation and superior sound quality.",
                ImageFile = "product-7.png",
                Price = 249.99M

            }

        };
    }
}
