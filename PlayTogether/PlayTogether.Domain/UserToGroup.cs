using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlayTogether.Domain
{
    public class UserToGroup: ISimpleEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid GroupId { get; set; }

        public virtual User User { get; set; }
        public virtual Group Grop { get; set; }
    }
}