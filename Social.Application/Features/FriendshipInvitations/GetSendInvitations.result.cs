using System;

namespace Social.Application.Features.FriendshipInvitations
{
    public partial class GetSentInvitations
    {
        public class Result
        {
            public Guid Id { get; set; }
            public string EmailAddress { get; set; }
            public string State { get; set; }
        }
    }
}