using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Social.Application.Features.Posts
{
    public partial class GetPosts
    {
        public class Handler : IRequestHandler<Command, IEnumerable<Result>>
        {
            private readonly IQuery<Empty, IEnumerable<Result>> _query;

            public Handler(IQuery<Empty, IEnumerable<Result>> query)
            {
                _query = query;
            }

            public async Task<IEnumerable<Result>> Handle(Command request, CancellationToken cancellationToken)
            {
                return await _query.Execute(Empty.Value);
            }
        }
    }
}