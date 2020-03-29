using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Social.Application.Features.FriendshipInvitations;
using Social.Application.Features.Members;
using Social.Application.Features.Posts;

namespace Social.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PostsController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<GetPosts.Result>> GetMembers() =>
            await _mediator.Send(new GetPosts.Command());

        [HttpPost]
        [Route("external")]
        public async Task CreateMember(CreateExternalPost.Command cmd) => await _mediator.Send(cmd);

        [HttpPost]
        [Route("like")]
        public async Task CreateMember(LikePost.Command cmd) => await _mediator.Send(cmd);
    }
}