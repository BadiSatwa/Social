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
            return Id != null;
        }
    }
}