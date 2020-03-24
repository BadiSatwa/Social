using System;

namespace Social.Domain
{
    public class FriendshipInvitationId : Value<Guid>
    {
        public FriendshipInvitationId(Guid value) : base(value)
        {
        }
    }
}