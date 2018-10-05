using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PlayTogether.Domain;

namespace PlayTogether.Web.Models
{
    public class SkillsProfileModel
    {
        [Required]
        public Guid ProfileId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        public ICollection<MusicGenre> MusicGenres { get; set; }
        public ICollection<MusicianRole> MusicianRoles { get; set; }
    }
}