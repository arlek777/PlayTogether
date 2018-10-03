using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlayTogether.Domain
{
    public class Profile: ISimpleEntity
    {
        public Profile()
        {
            MusicGenres = new List<MusicGenre>();
            MusicianRoles = new List<MusicianRole>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public bool IsActivated { get; set; } 
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

        public virtual ICollection<WorkType> WorkTypes { get; set; }
        public virtual ICollection<MusicGenre> MusicGenres { get; set; }
        public virtual ICollection<MusicianRole> MusicianRoles { get; set; }
    }

}