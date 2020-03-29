namespace Social.Domain.Posts
{
    public class FeederPost : Post
    {
        public FeederPost(PostId id)
        {
        }

        protected override void When(object @event)
        {
            throw new System.NotImplementedException();
        }
    }
}