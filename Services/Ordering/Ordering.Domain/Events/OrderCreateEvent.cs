using Ordering.Domain.Abstractions;
using Ordering.Domain.Models;

namespace Ordering.Domain.Events
{
    public record OrderCreateEvent(Order order) : IDomainEvent;

}
