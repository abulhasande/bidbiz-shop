using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Abstractions
{
    public abstract class Aggregate<TId> : Entity<TId>, IAggregate<TId>
    {
        private readonly List<IDomainEvent> _domainEvent = new();
        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvent.AsReadOnly();
        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvent.Add(domainEvent);
        }

        public IDomainEvent[] ClearDoaminEvents()
        {
            IDomainEvent[] dequeueEventss = _domainEvent.ToArray();

            _domainEvent.Clear();

            return dequeueEventss;
        }
    }
}
