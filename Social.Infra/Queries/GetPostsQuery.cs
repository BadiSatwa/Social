using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Social.Application;
using Social.Application.Features.Posts;
using Social.Infra.Projections;

namespace Social.Infra.Queries
{
    public class GetPostsQuery : IQuery<Empty, IEnumerable<GetPosts.Result>>
    {
        private readonly List<GetPostsViewModel> _viewModel;

        public GetPostsQuery(List<GetPostsViewModel> viewModel)
        {
            _viewModel = viewModel;
        }

        public Task<IEnumerable<GetPosts.Result>> Execute(Empty arg)
        {
            return Task.FromResult(_viewModel.Select(vm => 
                new GetPosts.Result
                {
                    Id = vm.Id,
                    LikeCount = vm.Who.Count(),
                    WhoLiked = vm.Who.Select(m => m.EmailAddress)
                }));
        }
    }
}