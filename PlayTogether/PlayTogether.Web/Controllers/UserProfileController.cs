using System;
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

        [HttpGet]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> GetMainInfo(Guid userId)
        {
            var user = await _crudService.Find<User>(u => u.Id == userId);
            if (user == null)
            {
                return NotFound();
            }

            var mainInfo = Mapper.Map<MainProfileModel>(user?.Profile);
            mainInfo.UserId = user.Id;
            return Ok(mainInfo);
        }

        [HttpGet]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> GetSkills(Guid userId)
        {
            var user = await _crudService.Find<User>(u => u.Id == userId);
            if (user == null)
            {
                return NotFound();
            }

            var skills = new SkillsProfileModel()
            {
                UserId = user.Id,
                ProfileId = user.ProfileId,
                MusicGenres = user.Profile.MusicGenres,
                MusicianRoles = user.Profile.MusicianRoles
            };
            return Ok(skills);
        }

        [HttpPost]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> UpdateMainProfile(MainProfileModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ValidationResultMessages.InvalidModelState);
            }

            var badResult = await CheckIfProfileBelongsToUser(model.UserId, model.ProfileId);
            if (badResult != null) return badResult;

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
            if (!ModelState.IsValid)
            {
                return BadRequest(ValidationResultMessages.InvalidModelState);
            }

            var badResult = await CheckIfProfileBelongsToUser(model.UserId, model.ProfileId);
            if (badResult != null) return badResult;

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
            });

            return Ok();
        }

        private async Task<IActionResult> CheckIfProfileBelongsToUser(Guid userId, Guid profileId)
        {
            var user = await _crudService.Find<User>(u => u.Id == userId);
            if (user == null)
            {
                return NotFound();
            }
            if (user.Profile.Id != profileId)
            {
                return BadRequest(ValidationResultMessages.InvalidModelState);
            }
            return null;
        }
    }
}