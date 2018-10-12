using System;
using System.ComponentModel.DataAnnotations;

namespace PlayTogether.Web.Models.Vacancy
{
    public class ChangeVacancyStatusModel
    {
        [Required]
        public Guid Id { get; set; }
    }
}