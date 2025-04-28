using BuildingBlocks.Exceptions;

namespace Basket.Api.Exception
{
    public class BaskNotFoundException : NotFoundException
    {
        public BaskNotFoundException(string userName) : base("Basket", userName)
        {

        }
    }
}
