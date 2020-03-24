using System;

namespace Social.Application.Features.Members
{
    public partial class GetMembers
    {
        public class Result
        {
            public Guid Id { get; set; }
            public string EmailAddress { get; set; }
        }
    }
}