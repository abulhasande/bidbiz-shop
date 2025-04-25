
using Catalog.Api.Products.GetProductById;
using Microsoft.Extensions.Logging;

namespace Catalog.Api.Products.GetProductByCategory
{

    public record GetProductByCategoryQuery(string category): IQuery<GetProductByCategoryResult>;

    public record GetProductByCategoryResult(IEnumerable<Product> Products);


    public class GetProductByCategoryQueryHandler(IDocumentSession session, ILogger<GetProductByCategoryQueryHandler> logger)
        : IRequestHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        public async  Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductByCategoryQueryHandler.Handle called with {@Query}", query);
            var products = await session.Query<Product>()
                                        .Where(p => p.Category.Contains(query.category))
                                        .ToListAsync();

            return new GetProductByCategoryResult(products);
        }
    }
}
