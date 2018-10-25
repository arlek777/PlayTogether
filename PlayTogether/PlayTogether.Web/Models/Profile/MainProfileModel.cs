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

        [MaxLength(10)]
        public ICollection<WorkType> WorkTypes { get; set; } // todo it's not implemented yet

        [MaxLength(10)]
        public ICollection<MusicGenre> MusicGenres { get; set; }

        [MaxLength(10)]
        public ICollection<MusicianRole> MusicianRoles { get; set; }
    }
}