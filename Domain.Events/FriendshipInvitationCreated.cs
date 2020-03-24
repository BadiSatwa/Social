using System;

namespace Social.Domain.Events
{
    public class FriendshipInvitationCreated
    {
        public Guid Id { get; set; }
        public Guid InvitedId { get; set; }
        public Guid InvitingId { get; set; }
        public string InvitationText { get; set; }
    }
}