using System;
using Social.Domain.Events;

namespace Social.Domain.Posts
{
    public class ExternalSystemPost : Post
    {
        public ExternalSystemPost(PostId id, ExternalSystemPostId externalSystemPostId)
        {
            Apply(new ExternalSystemPostCreated
            {
                Id = id,
                ExternalSystemPostId = externalSystemPostId.Value.Id,
                ExternalSystemPostType = externalSystemPostId.Value.Type.ToString()
            });
        }

        internal ExternalSystemPost()
        {}

        public ExternalSystemPostId ExternalSystemPostId { get; private set; }

        protected override void When(object @event)
        {
            base.When(@event);
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