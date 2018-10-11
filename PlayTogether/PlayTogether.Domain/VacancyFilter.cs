using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlayTogether.Domain
{
    public class VacancyFilter : ISimpleEntity
    {
        [ForeignKey("Vacancy")]
        public Guid Id { get; set; }
        public double MinRating { get; set; }
        public double MinExpirience { get; set; }
        public ICollection<string> Cities { get; set; }
        public ICollection<MusicGenre> MusicGenres { get; set; }
        public ICollection<MusicianRole> MusicianRoles { get; set; }
        public ICollection<WorkType> WorkTypes { get; set; }
        public virtual Vacancy Vacancy { get; set; }
    }
}