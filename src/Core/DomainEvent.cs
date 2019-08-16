using System;
using MediatR;

namespace Seedwork.DomainDriven.Core
{
    /// <summary>
    /// Abstract class of domain event.
    ///
    /// Implements <see cref="INotification"/> of <see cref="MediatR"/>.
    /// </summary>
    public abstract class DomainEvent : INotification
    {
        /// <summary>
        /// Initializes a new instance of DomainEvent.
        /// </summary>
        protected DomainEvent()
        {
            CreatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Gets creation date of event.
        /// </summary>
        public DateTime CreatedAt { get; }
    }
}