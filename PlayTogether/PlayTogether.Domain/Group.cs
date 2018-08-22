using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlayTogether.Domain
{
    public class Group: ISimpleEntity
    {
        public Group()
        {
            UserToGroups = new List<UserToGroup>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid CreatorId { get; set; }
        public Guid ProfileId { get; set; }
        public string Name { get; set; }

        public ICollection<UserToGroup> UserToGroups { get; set; }
        public virtual User Creator { get; set; }
        public virtual Profile Profile { get; set; }
    }
}