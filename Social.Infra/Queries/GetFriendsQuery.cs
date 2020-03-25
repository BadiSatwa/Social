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
        private readonly List<GetFriendsProjection.Member> _list;

        public GetFriendsQuery(List<GetFriendsProjection.Member> list)
        {
            _list = list;
        }

        public Task<IEnumerable<GetFriends.Result>> Execute(Empty arg)
        {
            return Task.FromResult(
                _list.First().Friends.Select(f => new GetFriends.Result
                {
                    Id = f.Id,
                    EmailAddress = f.EmailAddress
                })
            );
        }
    }
}