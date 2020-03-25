using System.Collections.Generic;
using MediatR;

namespace Social.Application.Features.FriendshipInvitations
{
    public partial class GetSentInvitations
    {
        public class Command : IRequest<IEnumerable<Result>>
        {

        }
    }
}