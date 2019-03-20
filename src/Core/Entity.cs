using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Seedwork.DomainDriven.Core
{
    public abstract class Entity : IEquatable<object>
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

        public override bool Equals(object obj)
        {
            if (!(obj is Entity))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (GetType() != obj.GetType())
                return false;

            var item = (Entity) obj;

            if (item.IsTransient() || IsTransient())
                return false;

            return item.Id.Equals(Id);
        }

        protected void RaiseDomainEvent(DomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        private bool IsTransient()
        {
            return Id == null || Id == 0;
        }

        private long GetId() => Id ?? throw new NullReferenceException(nameof(Id));

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
            return IsTransient() ? _transientId.GetHashCode() : GetId().GetHashCode();
        }
    }
}