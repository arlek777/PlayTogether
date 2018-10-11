using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlayTogether.BusinessLogic;
using PlayTogether.Domain;
using PlayTogether.Web.Infrastructure;
using PlayTogether.Web.Models.Profile;
using Profile = PlayTogether.Domain.Profile;

namespace PlayTogether.Web.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly ISimpleCRUDService _crudService;
        private readonly IMasterValueService _masterValueService;
        private readonly WebSession _webSession;

        public ProfileController(ISimpleCRUDService crudService, IMasterValueService masterValueService, WebSession webSession)
        {
            _crudService = crudService;
            _webSession = webSession;
            _masterValueService = masterValueService;
        }

        [HttpGet]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> GetMainInfo()
        {
            var user = await _crudService.Find<User>(u => u.Id == _webSession.UserId);
            var mainInfo = Mapper.Map<MainProfileModel>(user.Profile);
            return Ok(mainInfo);
        }

        [HttpGet]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> GetSkills()
        {
            var user = await _crudService.Find<User>(u => u.Id == _webSession.UserId);
            var skills = new SkillsProfileModel()
            {
                MusicGenres = await _masterValueService.GetMasterValuesByIds<MusicGenre>(user.Profile.MusicGenreIds),
                MusicianRoles = await _masterValueService.GetMasterValuesByIds<MusicianRole>(user.Profile.MusicianRoleIds)
            };
            return Ok(skills);
        }

        [HttpPost]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> UpdateMainProfile(MainProfileModel model)
        {
            await _crudService.Update<MainProfileModel, Profile>(_webSession.UserId, model, (to, from) =>
            {
                to.Name = from.Name;
                to.City = from.City;
                to.Age = from.Age;
                to.Experience = from.Experience;
                to.Description = from.Description;
                to.ContactEmail = from.ContactEmail;
                to.Phone1 = from.Phone1;
                to.Phone2 = from.Phone2;
                to.PhotoBase64 = from.PhotoBase64;

                to.WorkTypeIds.Clear();
                foreach (var wt in from.WorkTypes)
                {
                    to.WorkTypeIds.Add(wt.Id);
                }

                if(to.User.Type == UserType.Musician)
                {
                    var vacancy = to.User.Vacancies.FirstOrDefault();
                    vacancy.Title = from.VacancyFilterTitle;
                    vacancy.Description = from.Description;
                    vacancy.IsClosed = !from.IsVacancyOpen;
                    vacancy.VacancyFilter.Cities.Clear();
                    vacancy.VacancyFilter.Cities.Add(from.City);
                }
            });

            return Ok();
        }

        [HttpPost]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> UpdateSkills([FromBody] SkillsProfileModel model)
        {
            await _crudService.Update<SkillsProfileModel, Profile>(_webSession.UserId, model, (to, from) =>
            {
                to.MusicGenreIds.Clear();
                foreach (var mg in from.MusicGenres)
                {
                    to.MusicGenreIds.Add(mg.Id);
                }
                to.MusicianRoleIds.Clear();
                foreach (var mr in from.MusicianRoles)
                {
                    to.MusicianRoleIds.Add(mr.Id);
                }

                if (to.User.Type == UserType.Musician)
                {
                    var vacancy = to.User.Vacancies.FirstOrDefault();
                    vacancy.VacancyFilter.MusicGenreIds.Clear();
                    foreach (var mg in from.MusicGenres)
                    {
                        vacancy.VacancyFilter.MusicGenreIds.Add(mg.Id);
                    }
                    vacancy.VacancyFilter.MusicianRoleIds.Clear();
                    foreach (var mr in from.MusicianRoles)
                    {
                        vacancy.VacancyFilter.MusicianRoleIds.Add(mr.Id);
                    }
                }
            });

            return Ok();
        }
    }
}