using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Social.Domain.Posts;

namespace Social.Application.Features.Posts
{
    public partial class CreateExternalPost
    {
        public class Handler : IRequestHandler<Command>
        {
            private readonly IAggregateStore _store;

            public Handler(IAggregateStore store)
            {
                _store = store;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var post = new ExternalSystemPost(new PostId(Guid.NewGuid()),
                    new ExternalSystemPostId((request.ExternalPostId, Enum.Parse<ExternalSystemPostType>(request.ExternalSystemPostType))));
                await _store.Save<ExternalSystemPost, PostId>(post);
                return Unit.Value;
            }
        }
    }
}