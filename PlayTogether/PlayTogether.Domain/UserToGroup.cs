using System;

namespace PlayTogether.Domain
{
    public class UserToGroup
    {
        public Guid UserId { get; set; }
        public Guid GroupId { get; set; }
    }
}