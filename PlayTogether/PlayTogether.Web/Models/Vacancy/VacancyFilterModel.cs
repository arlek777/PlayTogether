using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PlayTogether.Domain;

namespace PlayTogether.Web.Models.Vacancy
{
    public class VacancyFilterModel
    {
        public Guid Id { get; set; }
        public double MinRating { get; set; }
        public double MinExpirience { get; set; }
        [Required]
        public ICollection<string> Cities { get; set; }
        [Required]
        public ICollection<MusicGenre> MusicGenres { get; set; }
        [Required]
        public ICollection<MusicianRole> MusicianRoles { get; set; }
        public ICollection<WorkType> WorkTypes { get; set; }
    }
}