using Basket.Api.Data;
using BuildingBlocks.CQRS;
using FluentValidation;
using System.Windows.Input;

namespace Basket.Api.Basket.DeleteBasket
{
    public record DeleteBasketCommand(string userName) : ICommandHandler<DeleteBasketResult>;
    public record  DeleteBasketResult(bool IsSuccess);

    public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
    {
        public DeleteBasketCommandValidator()
        {
            RuleFor(x => x.userName).NotEmpty().WithMessage("Username is required");
        }
    }

    public class DeleteBasketCommandHandler(IBasketRepository repository) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
    {
        public async  Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
        {
            //delete from DB
            await repository.DeleteBasket(command.userName, cancellationToken);

            return new DeleteBasketResult(true);
        }
    }
}
