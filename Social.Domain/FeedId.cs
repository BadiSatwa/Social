using System;

namespace Social.Domain
{
    public class FeedId : Value<Guid>
    {
        public FeedId(Guid value) : base(value)
        {
        }
    }
}