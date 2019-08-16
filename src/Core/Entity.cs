using System;

namespace Seedwork.DomainDriven.Core
{
    /// <summary>
    /// Abstract class of entity.
    /// </summary>
    public abstract class Entity<TId> : IEquatable<Entity<TId>>
    {
        private readonly Guid _transientId;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="id">Entity identification.</param>
        protected Entity(TId id) : this()
        {
            Id = id;
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        protected Entity()
        {
            CreatedAt = DateTime.UtcNow;
            _transientId = Guid.NewGuid();
        }

        /// <summary>
        /// Get Identification.
        /// </summary>
        public TId Id { get; protected set; }

        /// <summary>
        /// Get creation date.
        /// </summary>
        public DateTime CreatedAt { get; private set; }

        /// <summary>
        /// Compare current entity to another entity.
        /// </summary>
        /// <param name="other">Entity instance.</param>
        /// <returns>True if <paramref name="other"/> is equal to the current instance.</returns>
        public virtual bool Equals(Entity<TId> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            if (other.GetType() != GetType()) return false;

            if (other.IsTransient() || IsTransient())
                return false;

            return other.Id.Equals(Id);
        }

        /// <summary>
        /// Check if first value object and another are the same.
        /// </summary>
        /// <param name="left">First value object.</param>
        /// <param name="right">Last value object.</param>
        /// <returns>True if <paramref name="left"/> is equal to <paramref name="right"/>.</returns>
        public static bool operator ==(Entity<TId> left, Entity<TId> right)
        {
            if (ReferenceEquals(left, null) && ReferenceEquals(right, null)) return true;
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null)) return false;
            return left.Equals(right);
        }

        /// <summary>
        /// Check if first value object and another are different.
        /// </summary>
        /// <param name="left">First value object.</param>
        /// <param name="right">Last value object.</param>
        /// <returns>True if <paramref name="left"/> is equal to <paramref name="right"/>.</returns>
        public static bool operator !=(Entity<TId> left, Entity<TId> right)
        {
            return !(left == right);
        }

        /// <inheritdoc cref="Object"/>
        public override int GetHashCode()
        {
            return IsTransient() ? _transientId.GetHashCode() : GetId().GetHashCode();
        }

        /// <summary>
        /// Check if entity was not persisted.
        /// </summary>
        /// <returns>True if current instance is transient.</returns>
        private bool IsTransient()
        {
            return Equals(Id, default(TId));
        }

        /// <summary>
        /// Get identification.
        /// </summary>
        /// <returns>Returns identification.</returns>
        private object GetId()
        {
            if (IsTransient()) return _transientId;
            return Id;
        }

        /// <summary>
        /// Compare current entity to another object.
        /// </summary>
        /// <param name="obj">Object instance.</param>
        /// <returns>True if <paramref name="obj"/> is equal to the current instance.</returns>
        public override bool Equals(object obj)
        {
            return obj is Entity<TId> other && Equals(other);
        }
    }
}