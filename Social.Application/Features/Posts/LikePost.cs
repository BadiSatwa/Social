using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Social.Domain;
using Social.Domain.Posts;

namespace Social.Application.Features.Posts
{
    public partial class LikePost
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
                var post = await _store.Load<Post, PostId>(new PostId(request.Id));
                var who = await _store.Load<Member, MemberId>(new MemberId(request.ByWhoId));
                post.Like(who);
                await _store.Save<Post, PostId>(post);
                return Unit.Value;
            }
        }
    }
}