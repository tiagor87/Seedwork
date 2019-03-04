using System;
using MediatR;

namespace Seedwork.DomainDriven.Core
{
    public abstract class DomainEvent : INotification
    {
        protected DomainEvent()
        {
            CreatedAt = DateTime.UtcNow;
        }

        public DateTime CreatedAt { get; }
    }
}