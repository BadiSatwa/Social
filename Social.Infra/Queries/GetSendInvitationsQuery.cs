using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Social.Application;
using Social.Application.Features.FriendshipInvitations;
using Social.Infra.Projections;

namespace Social.Infra.Queries
{
    public class GetSendInvitationsQuery : IQuery<Empty, IEnumerable<GetSentInvitations.Result>>
    {
        private readonly List<GetSendInvitationsProjection.InvitationViewModel> _list;

        public GetSendInvitationsQuery(List<GetSendInvitationsProjection.InvitationViewModel> list)
        {
            _list = list;
        }

        public Task<IEnumerable<GetSentInvitations.Result>> Execute(Empty arg)
        {
            return Task.FromResult(_list.Select(i => new GetSentInvitations.Result
            {
                Id = i.Id,
                EmailAddress = i.InvitedEmailAddress,
                State = i.State
            }));
        }
    }
}