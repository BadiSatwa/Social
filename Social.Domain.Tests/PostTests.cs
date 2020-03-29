using System;
using System.Linq;
using Shouldly;
using Social.Domain.Events;
using Social.Domain.Posts;
using Xunit;

namespace Social.Domain.Tests
{
    public class PostTests
    {
        [Fact]
        public void UnlikePost_PostWasLiked_UnlikePost()
        {
            //Arrange
            var subject = new ExternalSystemPost(new PostId(Guid.NewGuid()), new ExternalSystemPostId(("10", ExternalSystemPostType.Event)));
            var member = new Member(new MemberId(Guid.NewGuid()), new EmailAddress("Test@Test.com"));

            subject.Like(member);
            
            //Act
            subject.StopLiking(member);

            //Assert
            subject.Liking.Count().ShouldBe(0);
        }

        [Fact]
        public void UnlikePost_PostWasLiked_EmitEvent()
        {
            //Arrange
            var subject = new ExternalSystemPost(new PostId(Guid.NewGuid()), new ExternalSystemPostId(("10", ExternalSystemPostType.Event)));
            var member = new Member(new MemberId(Guid.NewGuid()), new EmailAddress("Test@Test.com"));

            subject.Like(member);

            //Act
            subject.StopLiking(member);

            //Assert
            var @event = subject.GetEvents().OfType<PostUnliked>().Single();
            @event.Id.ShouldBe(subject.Id.Value);
            @event.MemberId.ShouldBe(member.Id.Value);
        }

        public void UnlikePost_PostWasNotLikedBefore_ThrowException()
        {

        }
    }
}