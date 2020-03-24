using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Social.Domain;

namespace Social.Application.Features.Members
{
    public partial class UpdateEmail
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
                var member = await _store.Load<Member, MemberId>(new MemberId(request.Id));
                member.UpdateEmail(new EmailAddress(request.EmailAddress));
                await _store.Save<Member, MemberId>(member);
                return Unit.Value;
            }
        }
    }
}