using MediatR;
using Social.Domain.Posts;

namespace Social.Application.Features.Posts
{
    public partial class CreateExternalPost
    {
        public class Command : IRequest
        {
            public string ExternalPostId { get; set; }
            public string ExternalSystemPostType { get; set; }
        }
    }
}