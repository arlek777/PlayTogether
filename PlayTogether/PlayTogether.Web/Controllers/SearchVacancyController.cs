using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlayTogether.BusinessLogic;
using PlayTogether.Domain;
using PlayTogether.Web.Models.Vacancy;

namespace PlayTogether.Web.Controllers
{
    [Authorize]
    public class SearchVacancyController : Controller
    {
        private readonly ISimpleCRUDService _crudService;

        public SearchVacancyController(ISimpleCRUDService crudService)
        {
            _crudService = crudService;
        }

        public async Task<IActionResult> GetGroupVacancies(VacancyFilterModel model = null)
        {
            ICollection<Vacancy> vacancies = new List<Vacancy>();
            if (model == null)
            {
                vacancies = await _crudService.Where<Vacancy>(v => !v.IsClosed && v.User.Type == UserType.Group);
            }
            else
            {
                Expression<Func<Vacancy, bool>> vacancyFilter = v => !v.IsClosed && v.User.Type == UserType.Group;
                vacancyFilter.AndAlso(model.ApplyCities() ? (v) => v.Cities.Any() : x => true);
                //vacancies = await _crudService.Where<Vacancy>(v => );
            }

            return Ok(vacancies);
        }
    }
}