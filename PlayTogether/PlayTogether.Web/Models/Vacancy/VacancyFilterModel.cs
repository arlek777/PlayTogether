using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using PlayTogether.Domain;
using PlayTogether.Domain.MasterValues;

namespace PlayTogether.Web.Models.Vacancy
{
    public class VacancyFilterModel
    {
        public Guid Id { get; set; }
        public UserType UserType { get; set; }
        public string VacancyTitle { get; set; }
        public double MinRating { get; set; }
        public double MinExperience { get; set; }

        [MaxLength(25)]
        public City[] Cities { get; set; }
        [MaxLength(25)]
        public MusicGenre[] MusicGenres { get; set; }
        [MaxLength(25)]
        public MusicianRole[] MusicianRoles { get; set; }
        [MaxLength(25)]
        public WorkType[] WorkTypes { get; set; }

        public bool ApplyTitle() => !String.IsNullOrEmpty(VacancyTitle);
        public bool ApplyMinRating() => MinRating != 0;
        public bool ApplyMinExperience() => MinExperience != 0;
        public bool ApplyCities() => Cities != null && Cities.Any();
        public bool ApplyMusicGenres() => MusicGenres != null && MusicGenres.Any();
        public bool ApplyMusicianRoles() => MusicianRoles != null && MusicianRoles.Any();
        public bool ApplyWorkTypes() => WorkTypes != null && WorkTypes.Any();
    }
}