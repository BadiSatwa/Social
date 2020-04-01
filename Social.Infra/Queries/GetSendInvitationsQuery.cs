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
        public Task<IEnumerable<GetSentInvitations.Result>> Execute(Empty arg)
        {
            return Task.FromResult(Enumerable.Empty<GetSentInvitations.Result>());
        }
    }
}