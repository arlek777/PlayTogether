using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlayTogether.BusinessLogic;
using PlayTogether.Domain;
using PlayTogether.WebClient.Infrastructure;

namespace PlayTogether.WebClient.Controllers
{
    public class AuthController : Controller
    {
        private readonly ISimpleCRUDService _crudService;
        private readonly JWTTokenProvider _jwtTokenProvider;

        public AuthController(ISimpleCRUDService crudService, JWTTokenProvider jwtTokenProvider)
        {
            _crudService = crudService;
            _jwtTokenProvider = jwtTokenProvider;
        }

        [Route("{controller}/{action}")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] string username, string password)
        {
            var user = await _crudService.Find<User>(u => u.UserName == username && u.PasswordHash == password);
            if (user == null)
            {
                user = await _crudService.CreateOrUpdate<User>(new User()
                {
                    UserName = username,
                    Profile = new Profile(),
                    PasswordHash = password
                });

                return Ok(GetJWTTokens(user));
            }

            return Ok(GetJWTTokens(user));
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
                Profile = new Profile(),
                PasswordHash = password
            });

            return Ok(GetJWTTokens(user));
        }

        private dynamic GetJWTTokens(User user)
        {
            return new
            {
                accessToken = _jwtTokenProvider.GetAccessToken(user),
                idToken = _jwtTokenProvider.GetIdToken(user)
            };
        }
    }
}