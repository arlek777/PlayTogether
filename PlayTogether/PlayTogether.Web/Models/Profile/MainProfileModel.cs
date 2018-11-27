using System.ComponentModel.DataAnnotations;
using PlayTogether.Domain.MasterValues;

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

        [Required]
        public string PhotoBase64 { get; set; }

        [MaxLength(25)]
        public WorkType[] WorkTypes { get; set; }

        [MaxLength(25)]
        public MusicGenre[] MusicGenres { get; set; }

        [MaxLength(25)]
        public MusicianRole[] MusicianRoles { get; set; }
    }
}