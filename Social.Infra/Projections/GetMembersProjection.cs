using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Social.Application.Features.Members;
using Social.Domain.Events;

namespace Social.Infra.Projections
{
    public class GetMembersProjection : IProjection
    {
        private readonly List<GetMembers.Result> _list;
        private readonly ILogger<GetMembersProjection> _log;

        public GetMembersProjection(List<GetMembers.Result> list, ILogger<GetMembersProjection> log)
        {
            _list = list;
            _log = log;
        }

        public Task Project(object @event)
        {
            _log.LogInformation($"Projecting event {@event.GetType().Name}");
            switch (@event)
            {
                case MemberCreated e:
                    _list.Add(new GetMembers.Result
                    {
                        Id = e.Id,
                        EmailAddress = e.EmailAddress
                    });
                    break;
                case MemberEmailChanged e:
                    _list.Single(m => m.Id == e.Id).EmailAddress = e.EmailAddress;
                    break;
            }
            
            return Task.CompletedTask;
        }
    }
}