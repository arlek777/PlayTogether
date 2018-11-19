using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlayTogether.BusinessLogic;
using PlayTogether.Domain;
using PlayTogether.Web.Infrastructure;
using PlayTogether.Web.Infrastructure.Extensions;
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
            PublicProfileModel model;
            Profile profile;
            if (id.HasValue)
            {
                profile = await _crudService.Find<Profile>(u => u.Id == id);
                model = Mapper.Map<PublicProfileModel>(profile);

                var contactRequest =
                    await _crudService.Find<ContactRequest>(v => (v.UserId == _webSession.UserId && v.ToUserId == id) ||
                                                                 (v.UserId == id && v.ToUserId == _webSession.UserId));
                if (contactRequest == null || contactRequest.Status != ContactRequestStatus.Approved)
                {
                    model.Address = "";
                    model.Url2 = "";
                    model.Url1 = "";
                    model.Phone1 = "";
                    model.Phone2 = "";
                    model.ContactEmail = "";
                    model.ContactTypes = "";
                    model.IsContactsAvailable = false;
                    model.IsContactRequestSent = contactRequest != null;
                }
                else
                {
                    model.IsContactsAvailable = true;
                    model.IsContactRequestSent = true;
                }
            }
            else
            {
                var user = await _crudService.Find<User>(u => u.Id == _webSession.UserId);
                profile = user.Profile;
                model = Mapper.Map<PublicProfileModel>(profile);
                model.IsContactsAvailable = true;
                model.IsContactRequestSent = true;
                model.IsSelfProfile = true;
            }

            return Ok(model);
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
            return Ok(user.Profile != null);
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
            await _crudService.Update<MainProfileModel, User>(_webSession.UserId, model, (to, from) =>
            {
                if(to.Profile == null)
                {
                    to.Profile = new Profile();
                }
            });

            await _crudService.Update<MainProfileModel, Profile>(_webSession.UserId, model, (to, from) =>
            {
                
                to.Name = from.Name;
                to.Description = from.Description;
                to.PhotoBase64 = from.PhotoBase64;
                to.JsonMusicGenres = from.MusicGenres.ToJson();

                if (to.User.Type == UserType.Musician)
                {
                    to.Age = from.Age;
                    to.Experience = from.Experience;
                    to.JsonWorkTypes = from.WorkTypes.ToJson();
                    to.JsonMusicianRoles = from.MusicianRoles.ToJson();
                    to.IsActivated = from.IsActivated;

                    var vacancy = to.User.Vacancies.FirstOrDefault();
                    vacancy.Title = from.Name;
                    vacancy.Description = from.Description;
                    vacancy.IsClosed = !from.IsActivated;
                    vacancy.VacancyFilter.JsonMusicianRoles = from.MusicianRoles.ToJson();
                    vacancy.VacancyFilter.JsonMusicGenres = from.MusicGenres.ToJson();
                }
                else if (to.User.Type == UserType.Group)
                {
                    to.GroupName = from.GroupName;
                }
            });

            return Ok();
        }

        [HttpPost]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> UpdateContactInfo([FromBody] ContactProfileModel model)
        {
            await _crudService.Update<ContactProfileModel, User>(_webSession.UserId, model, (to, from) =>
            {
                if (to.Profile == null)
                {
                    to.Profile = new Profile();
                }
            });

            await _crudService.Update<ContactProfileModel, Profile>(_webSession.UserId, model, (to, from) =>
            {
                to.JsonCity = from.City.ToJson();
                to.ContactEmail = from.ContactEmail;
                to.Phone1 = from.Phone1;
                to.Phone2 = from.Phone2;
                to.Address = from.Address;
                to.Url1 = from.Url1;
                to.Url2 = from.Url2;
                to.JsonContactTypes = from.ContactTypes.ToJson();
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