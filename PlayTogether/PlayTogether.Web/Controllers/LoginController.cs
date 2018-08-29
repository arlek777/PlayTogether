using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlayTogether.BusinessLogic;
using PlayTogether.Domain;

namespace PlayTogether.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly ISimpleCRUDService _crudService;

        public LoginController(ISimpleCRUDService crudService)
        {
            _crudService = crudService;
        }

        [Route("{controller}/{action}")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] string username, string password)
        {
            var user = await _crudService.Find<User>(u => u.UserName == username && u.PasswordHash == password);
            if (user == null)
            {
                return BadRequest();
            }

            return Ok(user);
        }

        [Route("{controller}/{action}")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] string username, string password)
        {
            var user = await _crudService.Find<User>(u => u.UserName == username);
            if (user != null)
            {
                return BadRequest();
            }

            user = await _crudService.CreateOrUpdate<User>(new User()
            {
                UserName = username,
                Profile = new Domain.Profile(),
                PasswordHash = password
            });

            return Ok(user);
        }
    }
}