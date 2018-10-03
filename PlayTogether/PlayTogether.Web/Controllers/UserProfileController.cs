using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlayTogether.BusinessLogic;
using PlayTogether.Domain;
using PlayTogether.Web.Models;
using Profile = PlayTogether.Domain.Profile;

namespace PlayTogether.Web.Controllers
{
    [Authorize]
    public class UserProfileController : Controller
    {
        private readonly ISimpleCRUDService _crudService;

        public UserProfileController(ISimpleCRUDService crudService)
        {
            _crudService = crudService;
        }

        [Route("[controller]/[action]")]
        public async Task<IActionResult> GetMainInfo(string userName)
        {
            var user = await _crudService.Find<User>(u => u.UserName == userName);
            var mainInfo = Mapper.Map<MainProfileModel>(user?.Profile);
            return Ok(mainInfo);
        }

        [Route("[controller]/[action]")]
        public async Task<IActionResult> GetSkillsInfo(string userName)
        {
            var user = await _crudService.Find<User>(u => u.UserName == userName);
            var skills = Mapper.Map<SkillsProfileModel>(user?.Profile);
            return Ok(skills);
        }

        [Route("[controller]/[action]")]
        public async Task<IActionResult> UpdateMainProfile(MainProfileModel model)
        {
            await _crudService.Update<MainProfileModel, Profile>(model.ProfileId, model, (to, from) =>
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
            });

            return Ok();
        }

        [Route("[controller]/[action]")]
        public async Task<IActionResult> UpdateSkills(SkillsProfileModel model)
        {
            await _crudService.Update<SkillsProfileModel, Profile>(model.ProfileId, model, (to, from) =>
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
                to.WorkTypes.Clear();
                foreach (var wt in from.WorkTypes)
                {
                    to.WorkTypes.Add(wt);
                }
            });

            return Ok();
        }
    }
}