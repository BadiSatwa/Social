using System;

namespace Social.Domain.Events
{
    public class PostUnliked
    {
        public Guid Id { get; set; }
        public Guid MemberId { get; set; }
    }
}