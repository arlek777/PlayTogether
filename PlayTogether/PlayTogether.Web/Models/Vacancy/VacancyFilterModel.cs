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
        public List<string> Cities { get; set; }
        [Required]
        public List<MusicGenre> MusicGenres { get; set; }
        [Required]
        public List<MusicianRole> MusicianRoles { get; set; }
        public List<WorkType> WorkTypes { get; set; }
    }
}