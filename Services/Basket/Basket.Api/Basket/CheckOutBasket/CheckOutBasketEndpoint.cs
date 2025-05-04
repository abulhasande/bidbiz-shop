using Basket.Api.Basket.GetBasket;
using Basket.Api.Dtos;
using Carter;
using Mapster;
using MediatR;

namespace Basket.Api.Basket.CheckOutBasket
{

    public record CheckOutBasketRequest(BasketCheckoutDto BasketCheckoutDto);
    public record CheckOutBasketResponse(bool IsSuccess);
    public class CheckOutBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket/checkout", async (CheckOutBasketRequest request, ISender sender) =>
            {
                var command = request.Adapt<CheckOutBasketCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<CheckOutBasketResponse>();

                return Results.Ok(response);
            })
             .WithName("GetBasketCheckout")
            .Produces<CheckOutBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Basket Checkout")
            .WithDescription("Get Basket Checkout");
        }
    }
}
