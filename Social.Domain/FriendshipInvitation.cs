using System;
using Social.Domain.Events;

namespace Social.Domain
{
    public class FriendshipInvitation : AggregateRoot<FriendshipInvitationId>
    {
        public FriendshipInvitation(FriendshipInvitationId friendshipInvitationId, MemberId invited, MemberId inviting, FriendshipInvitationText invitationText)
        {
            Apply(new FriendshipInvitationCreated
            {
                Id = Id,
                InvitedId = invited,
                InvitingId = inviting,
                InvitationText = invitationText
            });
        }

        public FriendshipInvitationId Id { get; private set; }
        public MemberId Invited { get; private set; }
        public MemberId Inviting { get; private set; }
        public FriendshipInvitationText InvitationText { get; private set; }
        public FriendshipInvitationState State { get; private set; }
        public DateTimeOffset? AcceptedAt { get; private set; }
        public DateTimeOffset? RejectedAt { get; private set; }

        public void Accept(DateTimeOffset acceptedAt)
        {
            Apply(new FriendshipInvitationAccepted
            {
                Id = Id,
                AcceptedAt = acceptedAt
            });
        }

        public void Reject(DateTimeOffset rejectedAt)
        {
            Apply(new FriendshipInvitationRejected
            {
                Id = Id,
                RejectedAt = rejectedAt
            });
        }

        protected override bool EnsureValidState()
        {
            return Id != null
                   && Invited != null
                   && Inviting != null
                   && InvitationText != null
                   && State switch
                   {
                       FriendshipInvitationState.Sent => AcceptedAt == null && RejectedAt == null,
                       FriendshipInvitationState.Accepted => AcceptedAt.HasValue && !RejectedAt.HasValue,
                       FriendshipInvitationState.Rejected => !AcceptedAt.HasValue && RejectedAt.HasValue,
                       _ => throw new ArgumentOutOfRangeException(nameof(State))
                   };
        }

        protected override void When(object @event)
        {
            switch (@event)
            {
                case FriendshipInvitationCreated e:
                    Id = new FriendshipInvitationId(e.Id);
                    Invited = new MemberId(e.InvitedId);
                    Inviting = new MemberId(e.InvitingId);
                    InvitationText = new FriendshipInvitationText(e.InvitationText);
                    State = FriendshipInvitationState.Sent;
                    break;
                case FriendshipInvitationAccepted e:
                    AcceptedAt = e.AcceptedAt;
                    State = FriendshipInvitationState.Accepted;
                    break;
                case FriendshipInvitationRejected e:
                    RejectedAt = e.RejectedAt;
                    State = FriendshipInvitationState.Rejected;
                    break;
            }
        }
    }

    public enum FriendshipInvitationState
    {
        Sent,
        Accepted,
        Rejected
    };
}