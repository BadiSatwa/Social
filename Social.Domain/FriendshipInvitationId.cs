using System;

namespace Social.Domain
{
    public class FriendshipInvitationId : ValueObject<Guid>
    {
        public FriendshipInvitationId(Guid value) : base(value)
        {
        }
    }
}