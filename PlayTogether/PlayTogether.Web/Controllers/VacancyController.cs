using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlayTogether.BusinessLogic;
using PlayTogether.Domain;
using PlayTogether.Web.Infrastructure;
using PlayTogether.Web.Models.Vacancy;

namespace PlayTogether.Web.Controllers
{
    [Authorize(Roles = "Group")]
    public class VacancyController : Controller
    {
        private readonly ISimpleCRUDService _crudService;
        private readonly WebSession _webSession;

        public VacancyController(ISimpleCRUDService crudService, WebSession webSession)
        {
            _crudService = crudService;
            _webSession = webSession;
        }

        [HttpGet]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> GetVacancies()
        {
            var vacancies = await _crudService.Where<Vacancy>(v => v.UserId == _webSession.UserId);
            return Ok(Mapper.Map<ICollection<VacancyModel>>(vacancies));
        }

        [HttpGet]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> GetVacancy(Guid id)
        {
            var vacancy = await _crudService.Find<Vacancy>(v => v.UserId == _webSession.UserId && v.Id == id);
            if (vacancy == null)
            {
                return NotFound();
            }
            return Ok(Mapper.Map<VacancyDetailModel>(vacancy));
        }

        [HttpGet]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> UpdateOrCreate([FromBody] VacancyDetailModel model)
        {
            var vacancy = await _crudService.Find<Vacancy>(v => v.UserId == _webSession.UserId && v.Id == model.Id);
            if (vacancy == null)
            {
                model.Date = DateTime.Now;
                vacancy = await _crudService.Create(Mapper.Map<Vacancy>(model));
            }
            else
            {
                await _crudService.Update<VacancyDetailModel, Vacancy>(model.Id, model, (to, from) =>
                    {
                        to.Description = from.Description;
                        to.Title = from.Title;
                        to.Status = from.Status;
                        to.VacancyFilter.Cities = from.VacancyFilter.Cities;
                        to.VacancyFilter.MinExpirience = from.VacancyFilter.MinExpirience;
                        to.VacancyFilter.MinRating = from.VacancyFilter.MinRating;
                        to.VacancyFilter.
                    });
            }

            return Ok(vacancy);
        }
    }
}