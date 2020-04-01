fromAll()
.when({
    $init: function () {
        return {
            members: [],
            posts: []
        };
    },
    MemberCreated: function (state, event) {
        state.members.push({
            Id: event.data.Id,
            EmailAddress: event.data.EmailAddress
        });
    },
    MemberEmailChanged: function (state, event) {
        var member = state.members.find(m => m.Id === event.data.Id);
        member.EmailAddress = event.data.EmailAddress;
    },
    ExternalSystemPostCreated: function (state, event) {
        state.posts.push({
            Id: event.data.Id,
            ExternalSystemPostId: event.data.ExternalSystemPostId,
            ExternalSystemPostType: event.data.ExternalSystemPostType,
            Likes: []
        });
    },
    PostLiked: function (state, event) {
        var post = state.posts.find(p => p.Id === event.data.Id);
        var member = state.members.find(m => m.Id === event.data.MemberId);
        post.Likes.push(member);
    }
})
.transformBy(function (state) {
    return state.posts.map(p => {
        return {
            Id: p.Id,
            LikeCount: p.Likes.length,
            WhoLiked: p.Likes.map(l => l.EmailAddress)
        };
    });
});