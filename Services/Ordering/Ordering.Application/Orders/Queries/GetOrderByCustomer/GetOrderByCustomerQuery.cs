using BuildingBlocks.CQRS;
using Ordering.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Orders.Queries.GetOrderByCustomer
{
    public record GetOrdesrByCustomerQuery(Guid customerId) 
        : IQuery<GetOrdersByCustomerResult>;
    public record GetOrdersByCustomerResult(IEnumerable<OrderDto> Orders);
}
