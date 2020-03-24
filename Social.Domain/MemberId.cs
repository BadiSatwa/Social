using System;

namespace Social.Domain
{
    public class MemberId : Value<Guid>
    {
        public MemberId(Guid value) : base(value)
        {
        }

        public override string ToString() => $"member-{_value}";

    }
}