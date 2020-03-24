using System;

namespace Social.Domain.Events
{
    public class MemberCreated
    {
        public Guid Id { get; set; }
        public string EmailAddress { get; set; }
    }
}