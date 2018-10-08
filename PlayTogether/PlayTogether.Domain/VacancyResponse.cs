using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlayTogether.Domain
{
    public class VacancyResponse : ISimpleEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid VacancyId { get; set; }
        public Guid UserId { get; set; }
        public string Message { get; set; }

        public virtual User User { get; set; }
        public virtual Vacancy Vacancy { get; set; }
    }
}