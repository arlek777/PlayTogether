using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlayTogether.BusinessLogic;
using PlayTogether.Domain;
using PlayTogether.Web.Infrastructure;
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
               
                
            }

            return Ok(vacancies);
        }
    }
}