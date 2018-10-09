using System;
using System.Collections.Generic;

namespace PlayTogether.Web.Models.Vacancy
{
    public class VacancyFilterModel
    {
        public Guid Id { get; set; }
        public double MinRating { get; set; }
        public double MinExpirience { get; set; }
        public ICollection<string> Cities { get; set; }
        public ICollection<Guid> MusicGenreIds { get; set; }
        public ICollection<Guid> MusicianRoleIds { get; set; }
        public ICollection<Guid> WorkTypeIds { get; set; }
    }
}