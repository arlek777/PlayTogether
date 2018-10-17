using System.Collections.Generic;

namespace PlayTogether.Web.Models.Vacancy
{
    public class VacancyDetailModel: VacancyModel
    {
        public ICollection<VacancyResponseModel> VacancyResponses { get; set; }
    }
}