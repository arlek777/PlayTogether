using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlayTogether.Domain
{
    public class VacancyFilter : BaseSkills, ISimpleEntity
    {
        public VacancyFilter()
        {
            JsonCities = "";
        }

        [ForeignKey("Vacancy")]
        public Guid Id { get; set; }
        public double MinRating { get; set; }
        public double MinExpirience { get; set; }
        public string JsonCities { get; set; }
        public virtual Vacancy Vacancy { get; set; }
    }
}