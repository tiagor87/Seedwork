using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Seedwork.DomainDriven.Core
{
    public abstract class Entity : IEquatable<Entity>
    {
        private readonly List<DomainEvent> _domainEvents;
        private readonly Guid _transientId;

        protected Entity(long id) : this()
        {
            Id = id;
        }

        protected Entity()
        {
            CreatedAt = DateTime.UtcNow;
            _domainEvents = new List<DomainEvent>();
            _transientId = Guid.NewGuid();
        }

        public long? Id { get; protected set; }
        public ReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        public DateTime CreatedAt { get; private set; }

        public bool Equals(Entity other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            if (other.GetType() != GetType()) return false;

            if (other.IsTransient() || IsTransient())
                return false;

            return other.Id.Equals(Id);
        }

        protected void RaiseDomainEvent(DomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        public static bool operator ==(Entity left, Entity right)
        {
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null)) return false;
            return left.Equals(right);
        }

        public static bool operator !=(Entity left, Entity right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            return GetId().GetHashCode();
        }

        private bool IsTransient()
        {
            return Id == null || Id == 0;
        }

        private object GetId()
        {
            if (IsTransient()) return _transientId;
            return Id;
        }

        public override bool Equals(object obj)
        {
            return obj is Entity other && Equals(other);
        }
    }
}