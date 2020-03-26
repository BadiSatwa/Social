using System;

namespace Social.Domain
{
    public abstract class ValueObject<T>
    {
        protected ValueObject(T value)
        {
            if(value == null) throw new ArgumentNullException(nameof(value), "Cant be null");
            Value = value;
        }

        public T Value { get; private set; }

        public static implicit operator T(ValueObject<T> valueObject) => valueObject.Value;
    }
}