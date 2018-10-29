using System;
using System.ComponentModel.DataAnnotations;

namespace PlayTogether.Web.Models.Vacancy
{
    public class VacancyModel
    {
        [Required]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        [Required]
        [MaxLength(256)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public bool IsClosed { get; set; }

        public VacancyFilterModel VacancyFilter { get; set; }
    }
}