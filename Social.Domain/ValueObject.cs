using System;
using System.Collections.Generic;

namespace Social.Domain
{
    public abstract class ValueObject<T> : IEquatable<T>
    {
        protected ValueObject(T value)
        {
            if(value == null) throw new ArgumentNullException(nameof(value), "Cant be null");
            Value = value;
        }

        public T Value { get; }

        public static implicit operator T(ValueObject<T> valueObject) => valueObject.Value;

        public bool Equals(T other)
        {
            return EqualityComparer<T>.Default.Equals(Value, other);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ValueObject<T>) obj);
        }

        public override int GetHashCode()
        {
            return EqualityComparer<T>.Default.GetHashCode(Value);
        }
    }
}