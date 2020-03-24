using System.Collections.Generic;
using Social.Domain.Events;

namespace Social.Domain
{
    public class Member : AggregateRoot<MemberId>
    {
        private readonly List<MemberId> _friends = new List<MemberId>();

        public Member(MemberId id, EmailAddress emailAddress)
        {
            Apply(new MemberCreated
            {
                Id = id,
                EmailAddress = emailAddress
            });
        }

        protected Member()
        { }

        public EmailAddress EmailAddress { get; private set; }

        public void AddFriend(Member friend)
        {
            Apply(new MemberFriendAdded
            {
                Id = Id,
                FriendId = friend.Id
            });
        }
        
        protected override bool EnsureValidState()
        {
            return Id != null
                   && EmailAddress != null;
        }

        protected override void When(object @event)
        {
            switch (@event)
            {
                case MemberCreated e:
                    Id = new MemberId(e.Id);
                    EmailAddress = new EmailAddress(e.EmailAddress);
                    break;
                case MemberFriendAdded e:
                    _friends.Add(new MemberId(e.FriendId));
                    break;
            }
        }

        public override string ToString() => Id.ToString();
    }
}