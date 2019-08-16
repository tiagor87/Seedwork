using System;
using System.Collections.Generic;
using System.Linq;

namespace Seedwork.DomainDriven.Core
{
    /// <summary>
    /// Abstract class of value object.
    /// </summary>
    public abstract class ValueObject : IEquatable<ValueObject>
    {
        /// <summary>
        /// Compare current value object to another value object.
        /// </summary>
        /// <param name="other">Value object instance.</param>
        /// <returns>True if <paramref name="other"/> is equal to the current instance.</returns>
        public virtual bool Equals(ValueObject other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            using (var thisValues = GetAtomicValues().GetEnumerator())
            using (var otherValues = other.GetAtomicValues().GetEnumerator())
            {
                while (thisValues.MoveNext() && otherValues.MoveNext())
                {
                    if (ReferenceEquals(thisValues.Current, null) ^ ReferenceEquals(otherValues.Current, null))
                        return false;
                    if (thisValues.Current != null && !thisValues.Current.Equals(otherValues.Current))
                        return false;
                }

                return !thisValues.MoveNext() && !otherValues.MoveNext();
            }
        }

        /// <summary>
        /// Compare current value object to another object.
        /// </summary>
        /// <param name="obj">Object instance.</param>
        /// <returns>True if <paramref name="obj"/> is equal to the current instance.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is ValueObject other && Equals(other);
        }

        /// <summary>
        /// Check if current value object and another value object are equal.
        /// </summary>
        /// <param name="left">First value object.</param>
        /// <param name="right">Last value object.</param>
        /// <returns>True if <paramref name="left"/> is equal to <paramref name="right"/>.</returns>
        public static bool operator ==(ValueObject left, ValueObject right)
        {
            if (ReferenceEquals(left, null) && ReferenceEquals(right, null)) return true;
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null)) return false;
            return left.Equals(right);
        }

        /// <summary>
        /// Check if current value object and another value object are different.
        /// </summary>
        /// <param name="left">First value object.</param>
        /// <param name="right">Last value object.</param>
        /// <returns>True if <paramref name="left"/> is equal to <paramref name="right"/>.</returns>
        public static bool operator !=(ValueObject left, ValueObject right)
        {
            return !(left == right);
        }

        /// <inheritdoc cref="Object"/>
        public override int GetHashCode()
        {
            return GetAtomicValues()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);
        }

        /// <summary>
        /// Gets values that make value object unique.
        /// </summary>
        /// <returns>List of values.</returns>
        protected abstract IEnumerable<object> GetAtomicValues();
    }
}