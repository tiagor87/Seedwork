using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Seedwork.DomainDriven.Core
{
    public abstract class Entity
    {
        private readonly List<DomainEvent> _domainEvents;
        private int? _requestedHashCode;

        protected Entity(long id) : this()
        {
            Id = id;
        }

        protected Entity()
        {
            CreatedAt = DateTime.UtcNow;
            _domainEvents = new List<DomainEvent>();
        }

        public long? Id { get; protected set; }
        public ReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        public DateTime CreatedAt { get; private set; }

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

        public static bool operator ==(Entity left, Entity right)
        {
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null)) return false;
            return left.Equals(right);
        }

        public static bool operator !=(Entity left, Entity right)
        {
            return !(left == right);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj is Entity == false)
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

        public override int GetHashCode()
        {
            if (IsTransient()) return base.GetHashCode();

            return _requestedHashCode.HasValue
                ? _requestedHashCode.Value
                : (_requestedHashCode = Id.GetHashCode()).Value;
        }
    }
}