using System;
using System.Collections.Generic;
using PlayTogether.Domain;

namespace PlayTogether.Web.Models
{
    public class SkillsProfileModel
    {
        public Guid ProfileId { get; set; }

        public ICollection<MusicGenre> MusicGenres { get; set; }
        public ICollection<MusicianRole> MusicianRoles { get; set; }
    }
}