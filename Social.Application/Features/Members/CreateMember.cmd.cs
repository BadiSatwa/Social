using System;
using MediatR;

namespace Social.Application.Features.Members
{
    public partial class CreateMember
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public string EmailAddress { get; set; }
        }
    }
}