using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity;

namespace PlayTogether.Domain
{
    public class User: IUser<Guid>, ISimpleEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid ProfileId { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }

        public virtual Profile Profile { get; set; }
        public virtual ICollection<UserToGroup> UserToGroups { get; set; }
    }
}