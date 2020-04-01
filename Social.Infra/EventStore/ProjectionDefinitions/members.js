fromCategory('member')
.when({
    $init: function () {
        return {
            members: []
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
    }
})
.transformBy(s => {
    return s.members;
});