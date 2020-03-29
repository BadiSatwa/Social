using System;
using System.Collections.Generic;

namespace Social.Application.Features.Posts
{
    public partial class GetPosts
    {
        public class Result
        {
            public Guid Id { get; set; }
            public int LikeCount { get; set; }
            public IEnumerable<string> WhoLiked { get; set; }
        }
    }
}