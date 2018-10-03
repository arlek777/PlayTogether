using System.Collections.Generic;
using PlayTogether.Domain;

namespace PlayTogether.Web.Models
{
    public class SkillsProfileModel
    {
        public string UserName { get; set; }

        // Checkboxes
        public virtual ICollection<WorkType> WorkTypes { get; set; }

        // Multiselectors
        public virtual ICollection<MusicGenre> MusicGenres { get; set; }
        public virtual ICollection<MusicianRole> MusicianRoles { get; set; }
    }
}