using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlayTogether.Domain
{
    public class Profile: ISimpleEntity
    {
        public Profile()
        {
            MusicGenreIds = new List<Guid>();
            MusicianRoleIds = new List<Guid>();
            WorkTypeIds = new List<Guid>();
        }

        [ForeignKey("User")]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ContactEmail { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public int Age { get; set; }
        public double Experience { get; set; }
        public string PhotoBase64 { get; set; }
        public double Rating { get; set; }
        public bool NotifyByEmail { get; set; }
        public virtual User User { get; set; }

        public ICollection<Guid> WorkTypeIds { get; set; }
        public ICollection<Guid> MusicGenreIds { get; set; }
        public ICollection<Guid> MusicianRoleIds { get; set; }
    }

}