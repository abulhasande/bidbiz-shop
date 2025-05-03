using BuildingBlocks.CQRS;
using BuildingBlocks.Pagination;
using Ordering.Application.Dtos;

namespace Ordering.Application.Orders.Queries.GetOrders
{
    public record  GetOrdersQuery (PaginationResult PaginationRequest)
        : IQuery<GetOrdersQueryResult>;

    public record GetOrdersQueryResult(PaginatedResult<OrderDto> Orders); 
}
