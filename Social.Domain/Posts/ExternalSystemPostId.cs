namespace Social.Domain.Posts
{
    public class ExternalSystemPostId : ValueObject<(string Id, ExternalSystemPostType Type)>
    {
        public ExternalSystemPostId((string Id, ExternalSystemPostType Type) value) : base(value)
        {
        }
    }

    public enum ExternalSystemPostType
    {
        Event,
        News
    }
}