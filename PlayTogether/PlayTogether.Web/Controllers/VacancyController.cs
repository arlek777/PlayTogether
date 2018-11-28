using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlayTogether.BusinessLogic;
using PlayTogether.Domain;
using PlayTogether.Web.Infrastructure;
using PlayTogether.Web.Infrastructure.Extensions;
using PlayTogether.Web.Infrastructure.Models;
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

            var filters = VacancyConditionalFilter.GetFilters(model).ToList();
            var vacancies = await _crudService.Where<Vacancy>(v => !v.IsClosed 
                                                                   && v.User.Profile != null 
                                                                   && v.User.Type == model.UserType);

            if (filters.Any())
            {
                var foundVacancies = vacancies.OrderBy(v => v.Date).ToList().Where(v => filters.All(f => f.PassFilter(v))).ToList();
                return Ok(Mapper.Map<List<PublicVacancyModel>>(foundVacancies));
            }

            return Ok(Mapper.Map<List<PublicVacancyModel>>(vacancies.ToList()));
        }

        [HttpGet]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> SearchVacanciesByUserProfile()
        {
            UserType userType = UserType.Uknown;
            if (_webSession.UserType == UserType.Group)
            {
                userType = UserType.Musician;
            }
            else if (_webSession.UserType == UserType.Musician)
            {
                userType = UserType.Group;
            }

            var userProfile = await _crudService.Find<User>(u => u.Id == _webSession.UserId);
            var filterModel = Mapper.Map<VacancyFilterModel>(userProfile.Vacancies.FirstOrDefault()?.VacancyFilter);

            var filters = VacancyConditionalFilter.GetFilters(filterModel).ToList();
            var vacancies = await _crudService.Where<Vacancy>(v => !v.IsClosed && v.User.Profile != null && v.User.Type == userType);
            var foundVacancies = vacancies.OrderBy(v => v.Date).ToList().Where(v => filters.All(f => f.PassFilter(v))).ToList();

            return Ok(new
            {
                vacancies = Mapper.Map<List<PublicVacancyModel>>(foundVacancies),
                filter = filterModel
            });
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
            var publicVacancy = Mapper.Map<PublicVacancyModel>(vacancy);

            var contactRequest =
               await _crudService.Find<ContactRequest>(v => v.UserId == _webSession.UserId
               && v.ToUserId == vacancy.UserId);
            publicVacancy.IsContactRequestSent = contactRequest != null;

            return Ok(publicVacancy);
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

                    to.VacancyFilter.MinExperience = from.VacancyFilter.MinExperience;
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