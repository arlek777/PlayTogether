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
            WorkCategories = new List<WorkCategory>();
        }

        [ForeignKey("User")]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string PhotoBase64 { get; set; }
        public string Description { get; set; }
        public double Rating { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<MusicGenre> MusicGenres { get; set; }
        public virtual ICollection<WorkCategory> WorkCategories { get; set; }
    }
}