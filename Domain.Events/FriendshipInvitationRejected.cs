using System;

namespace Social.Domain.Events
{
    public class FriendshipInvitationRejected
    {
        public Guid Id { get; set; }
        public DateTimeOffset RejectedAt { get; set; }
    }
}