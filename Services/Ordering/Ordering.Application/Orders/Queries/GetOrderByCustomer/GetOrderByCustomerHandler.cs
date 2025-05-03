using BuildingBlocks.CQRS;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using Ordering.Application.Extensions;
using Ordering.Application.Orders.Queries.GetOrdersByName;
using Ordering.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Ordering.Application.Orders.Queries.GetOrderByCustomer
{
    public class GetOrderByCustomerHandler(IApplicationDbContext dbContext) 
        : IQueryHandler<GetOrderByCustomerQuery, GetOrderByCustomerResult>
    {
        public async Task<GetOrderByCustomerResult> Handle(GetOrderByCustomerQuery query, CancellationToken cancellationToken)
        {
            var orders = await dbContext.Orders
                            .Include(o => o.OrderItems)
                            .AsNoTracking()
                            .Where(o => o.CustomerId == CustomerId.Of(query.CustomerId))
                            .OrderBy(o => o.OrderName.Value)
                            .ToListAsync(cancellationToken);

            return new GetOrderByCustomerResult(orders.ToOderDtoList());
        }
    }
}
