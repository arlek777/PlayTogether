using System;

namespace PlayTogether.Web.Models.Vacancy
{
    public class VacancyResponseModel
    {
        public Guid Id { get; set; }
        public Guid VacancyId { get; set; }
        public Guid UserId { get; set; }
        public string Message { get; set; }
    }
}