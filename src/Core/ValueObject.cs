using System;
using System.Collections.Generic;
using System.Linq;

namespace Seedwork.DomainDriven.Core
{
    public abstract class ValueObject : IEquatable<object>, IEqualityComparer<object>
    {
        public new bool Equals(object x, object y)
        {
            if (ReferenceEquals(x, null) || ReferenceEquals(y, null)) return false;
            return x.Equals(y);
        }

        public int GetHashCode(object obj)
        {
            return obj?.GetHashCode() ?? 0;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType()) return false;
            var other = (ValueObject) obj;
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