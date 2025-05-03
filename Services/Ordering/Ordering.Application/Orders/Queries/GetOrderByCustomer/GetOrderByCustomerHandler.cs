using BuildingBlocks.CQRS;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using Ordering.Application.Extensions;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Orders.Queries.GetOrderByCustomer
{
    public class GetOrderByCustomerHandler(IApplicationDbContext dbContext) 
        : IQueryHandler<GetOrdesrByCustomerQuery, GetOrdersByCustomerResult>
    {
        public async Task<GetOrdersByCustomerResult> Handle(GetOrdesrByCustomerQuery query, CancellationToken cancellationToken)
        {
            var orders = await dbContext.Orders
                            .Include(o => o.OrderItems)
                            .AsNoTracking()
                            .Where(o => o.CustomerId == CustomerId.Of(query.customerId))
                            .OrderBy(o => o.OrderName.Value)
                            .ToListAsync(cancellationToken);

           
            return new GetOrdersByCustomerResult(orders.ToOderDtoList());
        }
    }
}
