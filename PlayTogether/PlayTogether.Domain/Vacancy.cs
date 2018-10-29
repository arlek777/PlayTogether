using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlayTogether.Domain
{
    public class Vacancy: ISimpleEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime Date { get; set; }
        public bool IsClosed { get; set; }

        public virtual User User { get; set; }
        public virtual VacancyFilter VacancyFilter { get; set; }
    }
}