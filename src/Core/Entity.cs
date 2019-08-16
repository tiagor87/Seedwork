using System;

namespace Seedwork.DomainDriven.Core
{
    public abstract class Entity<TId> : IEquatable<Entity<TId>>
    {
        private readonly Guid _transientId;

        protected Entity(TId id) : this()
        {
            Id = id;
        }

        protected Entity()
        {
            CreatedAt = DateTime.UtcNow;
            _transientId = Guid.NewGuid();
        }

        public TId Id { get; protected set; }

        public DateTime CreatedAt { get; private set; }

        public virtual bool Equals(Entity<TId> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            if (other.GetType() != GetType()) return false;

            if (other.IsTransient() || IsTransient())
                return false;

            return other.Id.Equals(Id);
        }

        public static bool operator ==(Entity<TId> left, Entity<TId> right)
        {
            if (ReferenceEquals(left, null) && ReferenceEquals(right, null)) return true;
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null)) return false;
            return left.Equals(right);
        }

        public static bool operator !=(Entity<TId> left, Entity<TId> right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            return GetId().GetHashCode();
        }

        private bool IsTransient()
        {
            return Equals(Id, default(TId));
        }

        private object GetId()
        {
            if (IsTransient()) return _transientId;
            return Id;
        }

        public override bool Equals(object obj)
        {
            return obj is Entity<TId> other && Equals(other);
        }
    }
}