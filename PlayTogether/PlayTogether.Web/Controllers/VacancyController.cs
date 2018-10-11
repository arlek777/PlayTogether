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
        private readonly IMasterValueService _masterValueService;
        private readonly WebSession _webSession;

        public VacancyController(ISimpleCRUDService crudService, IMasterValueService masterValueService, WebSession webSession)
        {
            _crudService = crudService;
            _webSession = webSession;
            _masterValueService = masterValueService;
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
                        to.VacancyFilter.Cities = from.VacancyFilter.Cities;
                        to.VacancyFilter.MinExpirience = from.VacancyFilter.MinExpirience;
                        to.VacancyFilter.MinRating = from.VacancyFilter.MinRating;

                        to.VacancyFilter.MusicGenreIds.Clear();
                        foreach (var mg in from.VacancyFilter.MusicGenres)
                        {
                            to.VacancyFilter.MusicGenreIds.Add(mg.Id);
                        }
                        to.VacancyFilter.MusicianRoleIds.Clear();
                        foreach (var mr in from.VacancyFilter.MusicianRoles)
                        {
                            to.VacancyFilter.MusicianRoleIds.Add(mr.Id);
                        }
                        to.VacancyFilter.WorkTypeIds.Clear();
                        foreach (var wt in from.VacancyFilter.WorkTypes)
                        {
                            to.VacancyFilter.WorkTypeIds.Add(wt.Id);
                        }
                    });
            }

            return Ok(model);
        }
    }
}