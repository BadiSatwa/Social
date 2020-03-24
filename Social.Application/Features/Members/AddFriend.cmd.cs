using System;
using MediatR;

namespace Social.Application.Features.Members
{
    public partial class AddFriend
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public Guid FriendId { get; set; }
        }
    }
}