using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlayTogether.BusinessLogic;
using PlayTogether.Domain;
using PlayTogether.Web.Models;

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
        public async Task<IActionResult> GetProfile(string userName)
        {
            var user = await _crudService.Find<User>(u => u.UserName == userName);
            return Ok(user?.Profile);
        }

        [Route("[controller]/[action]")]
        public async Task<IActionResult> UpdateMainProfile(MainProfileModel model)
        {
            //var user = await _crudService.Find<User>(u => u.UserName == model.UserName);
            //if (user == null)
            //{
            //    return NotFound();
            //}

            //var profile = new Profile()
            //{
            //    Address = model.Address;
            //    Description = model.Description;
            //    ContactEmail = model.ContactEmail;
            //    Phone1 = model.Phone1;
            //    PhotoBase64 = model.PhotoBase64;
            //};
            //await _crudService.CreateOrUpdate<Profile>(profile, (to, from) =>
            //{
            //    to.Address = from.Address;
            //    to.Description = from.Description;
            //    to.ContactEmail = from.ContactEmail;
            //    to.Phone1 = from.Phone1;
            //    to.PhotoBase64 = from.PhotoBase64;
            //});

            return Ok();
        }

        [Route("[controller]/[action]")]
        public async Task<IActionResult> UpdateSkills(SkillsProfileModel model)
        {
            //await _crudService.CreateOrUpdate<Profile>(model, (to, from) =>
            //{
            //    to.Address = from.Address;
            //    to.Description = from.Description;
            //    to.ContactEmail = from.ContactEmail;
            //    to.Phone1 = from.Phone1;
            //    to.PhotoBase64 = from.PhotoBase64;
            //    to.MusicGenres = from.MusicGenres;
            //    to.WorkCategories = from.WorkCategories;
            //});

            return Ok();
        }
    }
}