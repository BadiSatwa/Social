using System.Collections.Generic;
using System.Threading.Tasks;
using Social.Application;
using Social.Application.Features.Members;
using Social.Infra.Projections;

namespace Social.Infra.Queries
{
    public class GetMembersQuery : IQuery<Empty, IEnumerable<GetMembers.Result>>
    {
        private readonly IProjectionsManager _projectionsManager;

        public GetMembersQuery(IProjectionsManager projectionsManager)
        {
            _projectionsManager = projectionsManager;
        }

        public Task<IEnumerable<GetMembers.Result>> Execute(Empty arg)
        {
            return _projectionsManager.GetResults<IEnumerable<GetMembers.Result>>("members");
        }
    }
}