using System;

namespace Social.Domain.Events
{
    public class FriendshipInvitationAccepted
    {
        public Guid Id { get; set; }
        public DateTimeOffset AcceptedAt { get; set; }
    }
}