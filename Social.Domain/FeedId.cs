using System;

namespace Social.Domain
{
    public class FeedId : ValueObject<Guid>
    {
        public FeedId(Guid value) : base(value)
        {
        }
    }
}