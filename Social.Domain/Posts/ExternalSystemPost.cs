using System;
using Social.Domain.Events;

namespace Social.Domain.Posts
{
    public class ExternalSystemPost : Post
    {
        public ExternalSystemPost(PostId id, ExternalSystemPostId externalSystemPostId) : base(id)
        {
            Apply(new ExternalSystemPostCreated
            {
                Id = id,
                ExternalSystemPostId = externalSystemPostId.Value.Id,
                ExternalSystemPostType = externalSystemPostId.Value.Type.ToString()
            });
        }

        public ExternalSystemPostId ExternalSystemPostId { get; private set; }

        protected override void When(object @event)
        {
            switch (@event)
            {
                case ExternalSystemPostCreated e:
                    Id = new PostId(e.Id);
                    ExternalSystemPostId = new ExternalSystemPostId((e.ExternalSystemPostId, Enum.Parse<ExternalSystemPostType>(e.ExternalSystemPostType)));
                    break;
            }
        }
    }
}