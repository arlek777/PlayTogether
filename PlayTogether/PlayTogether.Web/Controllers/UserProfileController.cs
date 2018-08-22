using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlayTogether.BusinessLogic;
using PlayTogether.Domain;

namespace PlayTogether.Web.Controllers
{
    public class UserProfileController : Controller
    {
        private readonly ISimpleCRUDService _crudService;

        public UserProfileController(ISimpleCRUDService crudService)
        {
            _crudService = crudService;
        }

        [Route("{controller}/{action}")]
        public async Task<IActionResult> GetProfile(Guid userId)
        {
            return Ok(await _crudService.Find<Profile>(p => p.UserId == userId));
        }

        [Route("{controller}/{action}")]
        public async Task<IActionResult> UpdateProfile(Profile model)
        {
            await _crudService.CreateOrUpdate<Profile>(model, (to, from) =>
            {
                to.Address = from.Address;
                to.Description = from.Description;
                to.Email = from.Email;
                to.Phone = from.Phone;
                to.PhotoBase64 = from.PhotoBase64;
                to.MusicGenres = from.MusicGenres;
                to.WorkCategories = from.WorkCategories;
            });

            return Ok();
        }
    }
}