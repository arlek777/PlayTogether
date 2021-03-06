using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlayTogether.BusinessLogic;
using PlayTogether.Domain;
using PlayTogether.Web.Infrastructure;
using PlayTogether.Web.Infrastructure.Models;
using PlayTogether.Web.Models;
using PlayTogether.Web.Models.Vacancy;

namespace PlayTogether.Web.Controllers
{
    [Authorize]
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
        public async Task<IActionResult> GetUserVacancies()
        {
            var vacancies = await _crudService.Where<Vacancy>(v => v.UserId == _webSession.UserId);
            return Ok(Mapper.Map<ICollection<VacancyModel>>(vacancies));
        }

        [HttpPost]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> SearchVacancies([FromBody] VacancyFilterModel model)
        {
            if (_webSession.UserType == UserType.Group)
            {
                model.UserType = UserType.Musician;
            }
            else if (_webSession.UserType == UserType.Musician)
            {
                model.UserType = UserType.Group;
            }

            var filters = VacancyConditionalFilter.GetFilters(model);
            var vacancies = await _crudService.Where<Vacancy>(v => !v.IsClosed && v.User.Type == model.UserType);
            var foundVacancies = vacancies.ToList().Where(v => filters.All(f => f.PassFilter(v))).ToList();

            return Ok(Mapper.Map<List<VacancyModel>>(foundVacancies));
        }

        [HttpGet]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> GetVacanciesByUserProfile()
        {
            var userProfile = await _crudService.Find<User>(u => u.Id == _webSession.UserId);
            var filterModel = Mapper.Map<VacancyFilterModel>(userProfile.Vacancies.FirstOrDefault());

            var filters = VacancyConditionalFilter.GetFilters(filterModel);
            var vacancies = await _crudService.Where<Vacancy>(v => filters.All(f => f.PassFilter(v)));

            return Ok(Mapper.Map<ICollection<VacancyModel>>(vacancies));
        }

        [HttpGet]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> GetUserVacancy(Guid id)
        {
            var vacancy = await _crudService.Find<Vacancy>(v => v.UserId == _webSession.UserId && v.Id == id);
            if (vacancy == null)
            {
                return NotFound();
            }

            var detail = Mapper.Map<VacancyModel>(vacancy);
            return Ok(detail);
        }

        [HttpGet]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> GetVacancy(Guid id)
        {
            var vacancy = await _crudService.Find<Vacancy>(v => v.Id == id);
            if (vacancy == null)
            {
                return NotFound();
            }

            var detail = Mapper.Map<VacancyModel>(vacancy);
            return Ok(detail);
        }

        [Authorize(Roles = "Group")]
        [HttpPost]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> UpdateOrCreate([FromBody] VacancyModel model)
        {
            var user = await _crudService.Find<User>(u => u.Id == _webSession.UserId);
            if (String.IsNullOrEmpty(user.Profile.Name))
            {
                return BadRequest(ValidationResultMessages.CantCreateVacancyProfileEmpty);
            }

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
                await _crudService.Update<VacancyModel, Vacancy>(model.Id, model, (to, from) =>
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

        [Authorize(Roles = "Group")]
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