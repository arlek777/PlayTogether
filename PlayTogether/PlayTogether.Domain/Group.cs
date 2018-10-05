using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlayTogether.Domain
{
    public class Group: ISimpleEntity
    {
        public Group()
        {
            Vacancies = new List<Vacancy>();
            SearchResponses = new List<VacancyResponse>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ProfileId { get; set; }

        public virtual User User { get; set; }
        public virtual Profile Profile { get; set; }
        public virtual ICollection<Vacancy> Vacancies { get; set; }
        public virtual ICollection<VacancyResponse> SearchResponses { get; set; }
    }
}