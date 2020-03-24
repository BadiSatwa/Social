using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Social.Application;
using Social.Application.Features.Members;

namespace Social.Infra.Queries
{
    public class GetMembersQuery : IQuery<Empty, IEnumerable<GetMembers.Result>>
    {
        private readonly List<GetMembers.Result> _list;

        public GetMembersQuery(List<GetMembers.Result> list)
        {
            _list = list;
        }

        public Task<IEnumerable<GetMembers.Result>> Execute(Empty arg)
        {
            return Task.FromResult(_list.AsEnumerable());
        }
    }
}