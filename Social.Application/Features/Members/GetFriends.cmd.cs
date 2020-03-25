using System;
using System.Collections.Generic;
using System.Reflection;
using MediatR;

namespace Social.Application.Features.Members
{
    public partial class GetFriends
    {
        public class Command : IRequest<IEnumerable<Result>>
        {
            public Guid Id { get; set; }
        }
    }
}