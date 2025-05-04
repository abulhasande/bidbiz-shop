using Basket.Api.Data;
using Basket.Api.Dtos;
using BuildingBlocks.CQRS;
using BuildingBlocks.Messaging.Events;
using FluentValidation;
using Mapster;
using MassTransit;
using System.Windows.Input;

namespace Basket.Api.Basket.CheckOutBasket
{
    public record CheckOutBasketCommand (BasketCheckoutDto BasketCheckoutDto)
        : ICommand<CheckoutBasketResult>;

    public record CheckoutBasketResult(bool IsSuccess);

    public class CheckoutBasketCommandValidator : AbstractValidator<CheckOutBasketCommand>
    {
        public CheckoutBasketCommandValidator()
        {
            RuleFor(x => x.BasketCheckoutDto).NotNull().WithMessage("Basketr CHeckout should not null");
            RuleFor(x => x.BasketCheckoutDto.UserName).NotEmpty().WithMessage("UserName is required");

        }
    }
    public class CheckOutBasketCommandHandler(IBasketRepository repository, IPublishEndpoint publishEntpoint)
        : ICommandHandler<CheckOutBasketCommand, CheckoutBasketResult>
    {
        public async Task<CheckoutBasketResult> Handle(CheckOutBasketCommand command, CancellationToken cancellationToken)
        {
            // get Exisitng Basket with Total Price

            var basket = await repository.GetBasket(command.BasketCheckoutDto.UserName, cancellationToken);

            if(basket == null)
            {
                return new CheckoutBasketResult(false);
            }

            //Set TotalPirce on the nasketchekout event message

            var eventMessage = command.BasketCheckoutDto.Adapt<BasketCheckoutEvent>();

            eventMessage.TotalPrice = basket.TotalPrice;

            //Send basket checkot evemt to the RabbitMQ using Masstransit

            await publishEntpoint.Publish(eventMessage, cancellationToken);

            //delete the  basket

            await repository.DeleteBasket(command.BasketCheckoutDto.UserName, cancellationToken);


            return new CheckoutBasketResult(true);

        }
    }
}
