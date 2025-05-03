using Carter;
using Mapster;
using MediatR;
using Ordering.Application.Dtos;
using Ordering.Application.Orders.Commands.DeleteOrder;

namespace Ordering.Api.Endpoint
{
   // public record DeleteOrderRequest(Guid Id);
    public record DeleteOrderResponse(bool IsSuccess);
    public class DeleteOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/orders/{id}", async(Guid Id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteOrderCommand(Id));
                var response = result.Adapt<DeleteOrderResponse>();

                return Results.Ok(response);
            })
             .WithName("DeletedOrder")
             .Produces<DeleteOrderResponse>(StatusCodes.Status200OK)
             .ProducesProblem(StatusCodes.Status400BadRequest)
             .ProducesProblem(StatusCodes.Status404NotFound)
             .WithSummary("Deleted Order")
             .WithDescription("Deleted Order");
        }
    }
}
