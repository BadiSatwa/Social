using System;

namespace Social.Domain.Events
{
    public class ExternalSystemPostCreated
    {
        public Guid Id { get; set; }
        public string ExternalSystemPostId { get; set; }
        public string ExternalSystemPostType { get; set; }
    }
}