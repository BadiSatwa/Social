namespace Social.Domain.Posts
{
    public abstract class Post : AggregateRoot<PostId>
    {
        protected Post(PostId id)
        {
            Id = id;
        }

        protected override bool EnsureValidState()
        {
            throw new System.NotImplementedException();
        }

        protected override void When(object @event)
        {
            throw new System.NotImplementedException();
        }
    }
}