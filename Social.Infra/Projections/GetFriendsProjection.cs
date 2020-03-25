using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Social.Domain.Events;

namespace Social.Infra.Projections
{
    public class GetFriendsProjection : IProjection
    {
        private readonly List<Member> _list;

        public GetFriendsProjection(List<Member> list)
        {
            _list = list;
        }

        public Task Project(object @event)
        {
            switch (@event)
            {
                case MemberCreated e:
                    _list.Add(new Member { Id = e.Id, EmailAddress = e.EmailAddress });
                    break;
                case MemberEmailChanged e:
                    _list.Single(m => m.Id == e.Id).EmailAddress = e.EmailAddress;
                    break;
                case MemberFriendAdded e:
                    _list.Single(m => m.Id == e.Id).Friends.Add(_list.Single(m => m.Id == e.FriendId));
                    break;

            }
            return Task.CompletedTask;
        }

        public class Member
        {
            public Guid Id { get; set; }
            public string EmailAddress { get; set; }
            public List<Member> Friends { get; } = new List<Member>();
        }
    }
}