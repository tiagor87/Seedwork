using System;
using System.Collections.Generic;
using System.Linq;

namespace Seedwork.DomainDriven.Core
{
    public abstract class ValueObject : IEquatable<ValueObject>
    {
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

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is ValueObject other && Equals(other);
        }

        public static bool operator ==(ValueObject left, ValueObject right)
        {
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null)) return false;
            return left.Equals(right);
        }

        public static bool operator !=(ValueObject left, ValueObject right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            return GetAtomicValues()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);
        }

        protected abstract IEnumerable<object> GetAtomicValues();
    }
}