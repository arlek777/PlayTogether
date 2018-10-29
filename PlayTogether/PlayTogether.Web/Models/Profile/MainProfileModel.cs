using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PlayTogether.Domain;

namespace PlayTogether.Web.Models.Profile
{
    public class MainProfileModel
    {
        public bool IsActivated { get; set; }

        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        [MaxLength(256)]
        public string GroupName { get; set; }

        [MaxLength(2048)]
        public string Description { get; set; }

        public int Age { get; set; }
        public double Experience { get; set; }

        [MaxLength(1)]
        public string PhotoBase64 { get; set; } // todo it's not implemented yet

        // TODO add validation for them
        public List<WorkType> WorkTypes { get; set; }

        public List<MusicGenre> MusicGenres { get; set; }

        public List<MusicianRole> MusicianRoles { get; set; }
    }
}