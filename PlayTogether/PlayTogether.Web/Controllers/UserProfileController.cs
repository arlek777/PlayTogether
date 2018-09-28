//using System;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using PlayTogether.BusinessLogic;
//using PlayTogether.Domain;

//namespace PlayTogether.Web.Controllers
//{
//    public class UserProfileController : Controller
//    {
//        private readonly ISimpleCRUDService _crudService;

//        public UserProfileController(ISimpleCRUDService crudService)
//        {
//            _crudService = crudService;
//        }

//        [Route("{controller}/{action}")]
//        public async Task<IActionResult> GetProfile(Guid id)
//        {
//            return Ok(await _crudService.Find<Profile>(p => p.Id == id));
//        }

//        [Route("{controller}/{action}")]
//        public async Task<IActionResult> UpdateProfile(Profile model)
//        {
//            await _crudService.CreateOrUpdate<Profile>(model, (to, from) =>
//            {
//                to.Address = from.Address;
//                to.Description = from.Description;
//                to.ContactEmail = from.ContactEmail;
//                to.Phone1 = from.Phone1;
//                to.PhotoBase64 = from.PhotoBase64;
//                to.MusicGenres = from.MusicGenres;
//                to.WorkCategories = from.WorkCategories;
//            });

//            return Ok();
//        }
//    }
//}