namespace Social.Domain.Posts
{
    public class CommentPost : Post
    {
        public CommentPost(PostId id)
        {
        }

        public PostId ParentId { get; private set; }

        public void Like(MemberId memberId)
        {

        }

        public void StopLiking(MemberId memberId)
        {

        }

        protected override void When(object @event)
        {
            throw new System.NotImplementedException();
        }
    }
}