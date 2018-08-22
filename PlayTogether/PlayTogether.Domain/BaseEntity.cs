using System.Collections.Generic;

namespace PlayTogether.Domain
{
    public abstract class BaseEntity
    {
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string PhotoBase64 { get; set; }
        public string Description { get; set; }
        public double Rating { get; set; }

        //public ICollection<Review> Reviews { get; set; }
        public ICollection<UserToGroup> UserToGroups { get; set; }
        public ICollection<MusicGenre> MusicGenres { get; set; }
    }
}