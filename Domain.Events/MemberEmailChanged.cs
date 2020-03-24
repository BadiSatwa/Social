using System;

namespace Social.Domain.Events
{
    public class MemberEmailChanged
    {
        public Guid Id { get; set; }
        public string EmailAddress { get; set; }
    }
}