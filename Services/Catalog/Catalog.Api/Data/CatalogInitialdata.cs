using Marten.Schema;

namespace Catalog.Api.Data
{
    public class CatalogInitialdata : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            using var session = store.LightweightSession();
            if (await session.Query<Product>().AnyAsync())
                return;

            session.Store<Product>(GetPreConfiguredProducts());
            await session.SaveChangesAsync();
        }


        private static IEnumerable<Product> GetPreConfiguredProducts() => new List<Product>()
        {
            new Product()
            {
                Id = new Guid("0e58f333-0a30-49b7-b534-48edbb9d37bd"),
                Name = "IPhone 16",
                Description = "This phone is the biggest change to it design",
                Category = new List<string>{"Smart Phone", "Mobile"},
                ImageFile = "iphone16.png",
                Price = 1150
            },
             new Product()
            {
                Id = new Guid("01966e06-779b-4169-a7f2-4ad7c94ab858"),
                Name = "IPhone 12",
                Description = "This phone is the biggest change to it design",
                Category = new List<string>{"Smart Phone", "Mobile"},
                ImageFile = "iphone12.png",
                Price = 1150
            },
            new Product()
            {
                Id = new Guid("b849aa2b-ce99-4485-af0c-457003fb7d60"),
                Name = "IPhone 13 Pro Max",
                Description = "This phone is the biggest change to it design",
                Category = new List<string>{"Smart Phone", "Mobile"},
                ImageFile = "iphone12.png",
                Price = 1250
            },
            new Product()
            {
                Id = new Guid("cd80b7c9-af91-4e54-a058-b9ec4229eab7"),
                Name = "Remdi Lite",
                Description = "This phone is the biggest change to it design",
                Category = new List<string>{"Smart Phone", "Mobile"},
                ImageFile = "redmi.png",
                Price = 750
            },
            new Product()
            {
                Id = new Guid("c893d989-00ff-4223-b8c5-21236fbd2310"),
                Name = "Sumsung Note",
                Description = "This phone is the biggest change to it design",
                Category = new List<string>{"Smart Phone", "Mobile"},
                ImageFile = "sumsungnote.png",
                Price = 1150
            },
            new Product()
            {
                Id = new Guid("60886404-869b-4735-88ff-abd550f5f174"),
                Name = "Sumsung 24FE",
                Description = "This phone is the biggest change to it design",
                Category = new List<string>{"Smart Phone", "Mobile"},
                ImageFile = "sumsung24fe.png",
                Price = 1150
            }
        };
        
        
    }
}
