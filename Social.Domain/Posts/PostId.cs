using System;

namespace Social.Domain.Posts
{
    public class PostId : ValueObject<Guid>
    {
        public PostId(Guid value) : base(value)
        {
        }

        public override string ToString() => $"post-{Value}";
    }   
}