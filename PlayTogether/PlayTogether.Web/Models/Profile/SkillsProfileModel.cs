using System.Collections.Generic;
using PlayTogether.Domain;

namespace PlayTogether.Web.Models.Profile
{
    public class SkillsProfileModel
    {
        public ICollection<MusicGenre> MusicGenres { get; set; }
        public ICollection<MusicianRole> MusicianRoles { get; set; }
    }
}