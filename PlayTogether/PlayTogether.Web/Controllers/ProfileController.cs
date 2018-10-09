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
                MusicGenres = user.Profile.MusicGenres,
                MusicianRoles = user.Profile.MusicianRoles
            };
            return Ok(skills);
        }

        [HttpPost]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> UpdateMainProfile(MainProfileModel model)
        {
            await _crudService.Update<MainProfileModel, Profile>(_webSession.UserId, model, (to, from) =>
            {
                to.IsActivated = from.IsActivated;
                to.Name = from.Name;
                to.City = from.City;
                to.Age = from.Age;
                to.Experience = from.Experience;
                to.Description = from.Description;
                to.ContactEmail = from.ContactEmail;
                to.Phone1 = from.Phone1;
                to.Phone2 = from.Phone2;
                to.PhotoBase64 = from.PhotoBase64;

                to.WorkTypes.Clear();
                foreach (var wt in from.WorkTypes)
                {
                    to.WorkTypes.Add(wt);
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
                to.MusicGenres.Clear();
                foreach (var mg in from.MusicGenres)
                {
                    to.MusicGenres.Add(mg);
                }
                to.MusicianRoles.Clear();
                foreach (var mr in from.MusicianRoles)
                {
                    to.MusicianRoles.Add(mr);
                }
            });

            return Ok();
        }
    }
}