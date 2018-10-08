using System;
using PlayTogether.Domain;

namespace PlayTogether.Web.Models.Vacancy
{
    public class VacancyModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public VacancyStatus Status { get; set; }
    }
}