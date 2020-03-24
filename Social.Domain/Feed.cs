using Social.Domain.Events;

namespace Social.Domain
{
    public class Feed : AggregateRoot<FeedId>
    {
        public Feed(FeedId feedId)
        {
            Apply(new FeedCreated
            {
                Id = feedId
            });
        }

        protected override bool EnsureValidState()
        {
            return Id != null;
        }

        protected override void When(object @event)
        {
            switch (@event)
            {
                case FeedCreated e:
                    Id = new FeedId(e.Id);
                    break;
            }
        }
    }
}