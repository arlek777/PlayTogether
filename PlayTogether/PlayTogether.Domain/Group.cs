using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlayTogether.Domain
{
    public class Group : BaseEntity
    {
        public Group()
        {
            UserToGroups = new List<UserToGroup>();
            MusicGenres = new List<MusicGenre>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid CreatorId { get; set; }
        public string Name { get; set; }

        public virtual User Creator { get; set; }
    }
}