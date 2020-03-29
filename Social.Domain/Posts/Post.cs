using System;
using System.Collections.Generic;
using Social.Domain.Events;

namespace Social.Domain.Posts
{
    public abstract class Post : AggregateRoot<PostId>
    {
        private readonly List<MemberId> _likes;
        protected Post()
        {
            _likes = new List<MemberId>();
        }

        public IEnumerable<MemberId> Liking => _likes;

        public void Like(Member who)
        {
            if (_likes.Contains(who.Id))
                throw new ArgumentException($"Post {Id.Value} was liked before by {who.Id.Value}", nameof(who));
            Apply(new PostLiked{ Id = Id, MemberId = who.Id});
        }

        public void StopLiking(Member who)
        {
            if (!_likes.Contains(who.Id))
                throw new ArgumentException($"Post {Id.Value} was not liked before by {who.Id.Value}", nameof(who));
            Apply(new PostUnliked{ Id = Id, MemberId = who.Id});
        }

        protected override bool EnsureValidState()
        {
            return Id != null;
        }

        protected override void When(object @event)
        {
            switch (@event)
            {
                case PostLiked e:
                    _likes.Add(new MemberId(e.MemberId));
                    break;
                case PostUnliked e:
                    _likes.Remove(new MemberId(e.MemberId));
                    break;
            }
        }
    }
}