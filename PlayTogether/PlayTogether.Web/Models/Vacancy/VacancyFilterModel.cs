using System;
using System.Collections.Generic;
using PlayTogether.Domain;

namespace PlayTogether.Web.Models.Vacancy
{
    public class VacancyFilterModel
    {
        public Guid Id { get; set; }
        public double MinRating { get; set; }
        public double MinExpirience { get; set; }
        public ICollection<string> Cities { get; set; }
        public ICollection<MusicGenre> MusicGenres { get; set; }
        public ICollection<MusicianRole> MusicianRoles { get; set; }
        public ICollection<WorkType> WorkTypes { get; set; }
    }
}