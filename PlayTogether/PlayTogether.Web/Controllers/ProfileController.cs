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
using PlayTogether.Web.Models.Profile;
using Profile = PlayTogether.Domain.Profile;

namespace PlayTogether.Web.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly ISimpleCRUDService _crudService;
        private readonly WebSession _webSession;

        public ProfileController(ISimpleCRUDService crudService, WebSession webSession)
        {
            _crudService = crudService;
            _webSession = webSession;
        }

        [HttpGet]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> GetPublicProfile(Guid? id = null)
        {
            Profile profile;
            if (id.HasValue)
            {
                profile = await _crudService.Find<Profile>(u => u.Id == id);
            }
            else
            {
                var user = await _crudService.Find<User>(u => u.Id == _webSession.UserId);
                profile = user.Profile;
            }
            return Ok(Mapper.Map<PublicProfileModel>(profile));
        }

        [HttpGet]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> GetUserProfileMainInfo()
        {
            var user = await _crudService.Find<User>(u => u.Id == _webSession.UserId);
            var mainInfo = Mapper.Map<MainProfileModel>(user.Profile);
            return Ok(mainInfo);
        }

        [HttpGet]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> IsUserProfileFilled()
        {
            var user = await _crudService.Find<User>(u => u.Id == _webSession.UserId);
            return Ok(!String.IsNullOrEmpty(user.Profile?.Name));
        }

        [HttpGet]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> GetUserProfileContactInfo()
        {
            var user = await _crudService.Find<User>(u => u.Id == _webSession.UserId);
            var contactInfo = Mapper.Map<ContactProfileModel>(user.Profile);
            return Ok(contactInfo);
        }

        [HttpPost]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> UpdateMainInfo([FromBody] MainProfileModel model)
        {
            await _crudService.Update<MainProfileModel, Profile>(_webSession.UserId, model, (to, from) =>
            {
                to.IsActivated = from.IsActivated;
                to.Name = from.Name;
                to.GroupName = from.GroupName;
                to.Age = from.Age;
                to.Experience = from.Experience;
                to.Description = from.Description;
                to.PhotoBase64 = from.PhotoBase64;
                to.JsonWorkTypes = from.WorkTypes.ToJson();
                to.JsonMusicianRoles = from.MusicianRoles.ToJson();
                to.JsonMusicGenres = from.MusicGenres.ToJson();

                if (to.User.Type == UserType.Musician)
                {
                    var vacancy = to.User.Vacancies.FirstOrDefault();
                    vacancy.Title = from.Name;
                    vacancy.Description = from.Description;
                    vacancy.IsClosed = !from.IsActivated;
                    vacancy.VacancyFilter.JsonMusicianRoles = from.MusicianRoles.ToJson();
                    vacancy.VacancyFilter.JsonMusicGenres = from.MusicGenres.ToJson();
                }
            });

            return Ok();
        }

        [HttpPost]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> UpdateContactInfo([FromBody] ContactProfileModel model)
        {
            await _crudService.Update<ContactProfileModel, Profile>(_webSession.UserId, model, (to, from) =>
            {
                to.City = from.City;
                to.ContactEmail = from.ContactEmail;
                to.Phone1 = from.Phone1;
                to.Phone2 = from.Phone2;
                if (to.User.Type == UserType.Musician)
                {
                    var vacancy = to.User.Vacancies.FirstOrDefault();
                    vacancy.VacancyFilter.JsonCities = new[] { from.City }.ToJson();
                }
            });

            return Ok();
        }
    }
}