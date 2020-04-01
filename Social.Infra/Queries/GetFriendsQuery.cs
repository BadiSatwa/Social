using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Social.Application;
using Social.Application.Features.Members;
using Social.Infra.Projections;

namespace Social.Infra.Queries
{
    public class GetFriendsQuery : IQuery<Empty, IEnumerable<GetFriends.Result>>
    {
        public Task<IEnumerable<GetFriends.Result>> Execute(Empty arg)
        {
            return Task.FromResult(Enumerable.Empty<GetFriends.Result>());
        }
    }
}