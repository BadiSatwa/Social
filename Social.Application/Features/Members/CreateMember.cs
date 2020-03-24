using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Social.Domain;

namespace Social.Application.Features.Members
{
    public partial class CreateMember
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
                var member = new Member(new MemberId(request.Id), new EmailAddress(request.EmailAddress));
                await _store.Save<Member, MemberId>(member);
                return Unit.Value;
            }
        }
    }
}