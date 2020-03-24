using System;

namespace Social.Domain.Posts
{
    public class PostId : Value<Guid>
    {
        public PostId(Guid value) : base(value)
        {
        }
    }
}