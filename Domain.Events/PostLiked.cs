using System;

namespace Social.Domain.Events
{
    public class PostLiked
    {
        public Guid Id { get; set; }
        public Guid MemberId { get; set; }
    }
}