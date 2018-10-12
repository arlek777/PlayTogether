using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using PlayTogether.Domain;

namespace PlayTogether.Web.Models.Vacancy
{
    public class VacancyFilterModel
    {
        public Guid Id { get; set; }
        public double MinRating { get; set; }
        public double MinExpirience { get; set; }
        [Required]
        public List<string> Cities { get; set; }
        [Required]
        public List<MusicGenre> MusicGenres { get; set; }
        [Required]
        public List<MusicianRole> MusicianRoles { get; set; }
        public List<WorkType> WorkTypes { get; set; }

        public bool ApplyMinRating() => MinRating != 0;
        public bool ApplyMinExpirience() => MinExpirience != 0;
        public bool ApplyCities() => Cities != null && Cities.Any();
        public bool ApplyMusicGenres() => MusicGenres != null && MusicGenres.Any();
        public bool ApplyMusicianRoles() => MusicianRoles != null && MusicianRoles.Any();
        public bool ApplyWorkTypes() => WorkTypes != null && WorkTypes.Any();
    }
}