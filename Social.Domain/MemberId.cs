using System;

namespace Social.Domain
{
    public class MemberId : ValueObject<Guid>
    {
        public MemberId(Guid value) : base(value)
        {
        }

        public override string ToString() => $"member-{Value}";

    }
}