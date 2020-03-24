using System;

namespace Social.Domain.Events
{
    public class MemberFriendAdded
    {
        public Guid Id { get; set; }
        public Guid FriendId { get; set; }
    }
}