using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Social.Application.Features.FriendshipInvitations;
using Social.Application.Features.Members;

namespace Social.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MembersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MembersController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<GetMembers.Result>> GetMembers() =>
            await _mediator.Send(new GetMembers.Command());

        [HttpGet]
        [Route("invitations/sent")]
        public async Task<IEnumerable<GetSentInvitations.Result>> GetSentInvitations() =>
            await _mediator.Send(new GetSentInvitations.Command());

        [HttpPost]
        public async Task CreateMember(CreateMember.Command cmd) => await _mediator.Send(cmd);

        [HttpPost]
        [Route("friends")]
        public async Task Get(AddFriend.Command cmd) => await _mediator.Send(cmd);

        [HttpGet]
        [Route("{id}/friends")]
        public async Task<IEnumerable<GetFriends.Result>> Get(Guid id) => await _mediator.Send(new GetFriends.Command { Id = id });

        [HttpPatch]
        [Route("email")]
        public async Task Get(UpdateEmail.Command cmd) => await _mediator.Send(cmd);
    }
}