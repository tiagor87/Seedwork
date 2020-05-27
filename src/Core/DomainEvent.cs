using System;

namespace TRDomainDriven.Core
{
    /// <summary>
    /// Abstract class of domain event.
    ///
    /// Implements <see cref="INotification"/> of <see cref="MediatR"/>.
    /// </summary>
    public abstract class DomainEvent
    {
        /// <summary>
        /// Initializes a new instance of DomainEvent.
        /// </summary>
        protected DomainEvent()
        {
            CreatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Get creation date.
        /// </summary>
        public DateTime CreatedAt { get; }
    }
}