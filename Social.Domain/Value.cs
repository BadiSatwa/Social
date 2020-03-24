using System;

namespace Social.Domain
{
    public abstract class Value<T>
    {
        protected readonly T _value;

        protected Value(T value)
        {
            if(value == null) throw new ArgumentNullException(nameof(value), "Cant be null");
            _value = value;
        }

        public static implicit operator T(Value<T> value) => value._value;
    }
}