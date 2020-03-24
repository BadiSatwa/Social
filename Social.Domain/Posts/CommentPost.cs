﻿namespace Social.Domain.Posts
{
    public class CommentPost : Post
    {
        public CommentPost(PostId id) : base(id)
        {
        }

        public PostId ParentId { get; private set; }

        public void Like(MemberId memberId)
        {

        }

        public void StopLiking(MemberId memberId)
        {

        }
    }
}