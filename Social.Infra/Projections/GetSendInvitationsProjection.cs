using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Social.Application.Features.FriendshipInvitations;
using Social.Application.Features.Members;
using Social.Domain;
using Social.Domain.Events;

namespace Social.Infra.Projections
{
    public class GetSendInvitationsProjection : IProjection
    {
        private readonly List<GetMembers.Result> _members;
        private readonly ILogger<GetMembersProjection> _log;

        private readonly List<InvitationViewModel> _list;

        public GetSendInvitationsProjection(List<GetMembers.Result> members, ILogger<GetMembersProjection> log, List<InvitationViewModel> list)
        {
            _members = members;
            _log = log;
            _list = list;
        }

        public Task Project(object @event)
        {
            _log.LogInformation($"Projecting event {@event.GetType().Name}");
            switch (@event)
            {
                case FriendshipInvitationCreated e:
                    _list.Add(new InvitationViewModel
                    {
                        Id = e.Id,
                        InvitingId = e.InvitingId,
                        InvitingEmailAddress = _members.Single(m => m.Id == e.InvitingId).EmailAddress,
                        InvitedId = e.InvitedId,
                        InvitedEmailAddress = _members.Single(m => m.Id == e.InvitedId).EmailAddress,
                        State = FriendshipInvitationState.Sent.ToString()
                    });
                    break;
            }

            return Task.CompletedTask;
        }

        public class InvitationViewModel
        {
            public Guid Id { get; set; }
            public Guid InvitingId { get; set; }
            public string InvitingEmailAddress { get; set; }
            public Guid InvitedId { get; set; }
            public string InvitedEmailAddress { get; set; }
            public string State { get; set; }
        }
    }
}