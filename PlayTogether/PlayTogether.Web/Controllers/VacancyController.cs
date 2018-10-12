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

            var detail = Mapper.Map<VacancyDetailModel>(vacancy);
            return Ok(detail);
        }

        [HttpPost]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> UpdateOrCreate([FromBody] VacancyDetailModel model)
        {
            var vacancy = await _crudService.Find<Vacancy>(v => v.UserId == _webSession.UserId && v.Id == model.Id);
            if (vacancy == null)
            {
                model.Date = DateTime.Now;
                vacancy = Mapper.Map<Vacancy>(model);
                vacancy.UserId = _webSession.UserId;
                vacancy = await _crudService.Create(vacancy);
                model.Id = vacancy.Id;
            }
            else
            {
                await _crudService.Update<VacancyDetailModel, Vacancy>(model.Id, model, (to, from) =>
                {
                    to.Description = from.Description;
                    to.Title = from.Title;
                    to.IsClosed = from.IsClosed;

                    to.VacancyFilter.MinExpirience = from.VacancyFilter.MinExpirience;
                    to.VacancyFilter.MinRating = from.VacancyFilter.MinRating;

                    to.VacancyFilter.JsonMusicianRoles = from.VacancyFilter.MusicianRoles.ToJson();
                    to.VacancyFilter.JsonMusicGenres = from.VacancyFilter.MusicGenres.ToJson();
                    to.VacancyFilter.JsonCities = from.VacancyFilter.Cities.ToJson();
                    to.VacancyFilter.JsonWorkTypes = from.VacancyFilter.WorkTypes.ToJson();
                });
            }

            return Ok(model);
        }

        [HttpPost]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> ChangeVacancyStatus([FromBody] ChangeVacancyStatusModel model)
        {
            var vacancy = await _crudService.Find<Vacancy>(v => v.UserId == _webSession.UserId && v.Id == model.Id);
            if (vacancy == null)
            {
                return NotFound();
            }

            await _crudService.Update<ChangeVacancyStatusModel, Vacancy>(model.Id, model, (to, from) =>
                {
                    to.IsClosed = !to.IsClosed;
                });
            return Ok();
        }
    }
}