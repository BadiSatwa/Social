using System;
using System.Collections.Generic;
using MediatR;

namespace Social.Application.Features.Members
{
    public partial class GetMembers
    {
        public class Command : IRequest<IEnumerable<Result>>
        {
        }
    }
}