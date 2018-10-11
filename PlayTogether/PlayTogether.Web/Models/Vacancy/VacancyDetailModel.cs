using System.Collections.Generic;

namespace PlayTogether.Web.Models.Vacancy
{
    public class VacancyDetailModel: VacancyModel
    {
        public string Description { get; set; }
        public bool NotifyByEmail { get; set; }

        public VacancyFilterModel VacancyFilter { get; set; }
        public ICollection<VacancyResponseModel> VacancyResponses { get; set; }
    }
}