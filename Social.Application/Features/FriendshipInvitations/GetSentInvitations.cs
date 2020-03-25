using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Social.Application.Features.FriendshipInvitations
{
    public partial class GetSentInvitations
    {
        public class Handler : IRequestHandler<Command, IEnumerable<Result>>
        {
            private readonly IQuery<Empty, IEnumerable<GetSentInvitations.Result>> _query;

            public Handler(IQuery<Empty, IEnumerable<Result>> query)
            {
                _query = query;
            }

            public Task<IEnumerable<Result>> Handle(Command request, CancellationToken cancellationToken)
            {
                return _query.Execute(Empty.Value);
            }
        }
    }
}