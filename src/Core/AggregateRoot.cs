using System.Collections.Generic;

namespace Seedwork.DomainDriven.Core
{
    /// <summary>
    /// Abstract class of Aggregate Root.
    /// </summary>
    /// <typeparam name="TId">Id type.</typeparam>
    public abstract class AggregateRoot<TId> : Entity<TId>
    {
        private readonly List<DomainEvent> _domainEvents = new List<DomainEvent>();

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="id">Aggegate root identification.</param>
        protected AggregateRoot(TId id) : base(id)
        {
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        protected AggregateRoot()
        {
        }

        /// <summary>
        /// Get read only list of domain events. 
        /// </summary>
        public IEnumerable<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        /// <summary>
        /// Raises a new domain event.
        /// </summary>
        /// <param name="domainEvent"></param>
        protected void RaiseDomainEvent(DomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        /// <summary>
        /// Clear list of domain events.
        /// </summary>
        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}