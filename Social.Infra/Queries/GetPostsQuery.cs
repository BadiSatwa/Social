using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using EventStore.ClientAPI.Common.Log;
using EventStore.ClientAPI.Projections;
using EventStore.ClientAPI.SystemData;
using Microsoft.Extensions.Logging;
using Social.Application;
using Social.Application.Features.Posts;
using Social.Infra.Projections;

namespace Social.Infra.Queries
{
    public class GetPostsQuery : IQuery<Empty, IEnumerable<GetPosts.Result>>
    {
        private readonly IProjectionsManager _projectionsManager;

        public GetPostsQuery(IProjectionsManager projectionsManager)
        {
            _projectionsManager = projectionsManager;
        }
            
        public Task<IEnumerable<GetPosts.Result>> Execute(Empty arg)
        {
            return _projectionsManager.GetResults<IEnumerable<GetPosts.Result>>("likes");
        }
    }
}