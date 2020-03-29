using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Social.Domain.Events;

namespace Social.Infra.Projections
{
    public class GetPostsProjection : IProjection
    {
        private readonly List<GetPostsViewModel> _viewModel;
        private readonly List<MemberLookupViewModel> _memberLookup;

        public GetPostsProjection(List<MemberLookupViewModel> memberLookup, List<GetPostsViewModel> viewModel)
        {
            _memberLookup = memberLookup;
            _viewModel = viewModel;
        }

        public Task Project(object @event)
        {
            switch (@event)
            {
                case ExternalSystemPostCreated e:
                    _viewModel.Add(new GetPostsViewModel
                    {
                        Id = e.Id, 
                        Who = Enumerable.Empty<MemberLookupViewModel>()
                    });
                    break;
                case PostLiked e:
                    var post = _viewModel.Single(r => r.Id == e.Id);
                    post.Who = post.Who.Append(_memberLookup.Single(m => m.Id == e.MemberId));
                    break;
            }

            return Task.CompletedTask;
        }
    }

    public class GetPostsViewModel
    {
        public Guid Id { get; set; }

        public IEnumerable<MemberLookupViewModel> Who { get; set; }
    }
}