using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlayTogether.Domain
{
    public class Vacancy: ISimpleEntity
    {
        public Vacancy()
        {
            VacancyResponses = new List<VacancyResponse>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid VacancyFilterId { get; set; } // todo remove it in future it's useless
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime Date { get; set; }
        public bool IsClosed { get; set; }

        public virtual User User { get; set; }
        public virtual VacancyFilter VacancyFilter { get; set; }
        public virtual ICollection<VacancyResponse> VacancyResponses { get; set; }
    }
}