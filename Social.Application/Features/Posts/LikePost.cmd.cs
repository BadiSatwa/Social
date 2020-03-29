using System;
using MediatR;

namespace Social.Application.Features.Posts
{
    public partial class LikePost
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public Guid ByWhoId { get; set; }
        }
    }
}