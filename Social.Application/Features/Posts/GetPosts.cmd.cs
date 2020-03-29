using System.Collections.Generic;
using MediatR;

namespace Social.Application.Features.Posts
{
    public partial class GetPosts
    {
        public class Command : IRequest<IEnumerable<Result>>
        {}
    }
}