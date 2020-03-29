using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Social.Domain.Events;

namespace Social.Infra.Projections
{
    public class MembersLookupProjection : IProjection
    {
        private readonly List<MemberLookupViewModel> _list;

        public MembersLookupProjection(List<MemberLookupViewModel> list)
        {
            _list = list;
        }

        public Task Project(object @event)
        {
            switch (@event)
            {
                case MemberCreated e:
                    _list.Add(new MemberLookupViewModel{ Id = e.Id, EmailAddress = e.EmailAddress});
                    break;
                case MemberEmailChanged e:
                    _list.Single(m => m.Id == e.Id).EmailAddress = e.EmailAddress;
                    break;
            }
            return Task.CompletedTask;
        }
    }

    public class MemberLookupViewModel
    {
        public Guid Id { get; set; }
        public string EmailAddress { get; set; }
    }
}