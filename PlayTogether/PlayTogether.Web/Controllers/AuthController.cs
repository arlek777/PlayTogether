using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using PlayTogether.BusinessLogic;
using PlayTogether.Domain;
using PlayTogether.WebClient.Infrastructure;
using PlayTogether.WebClient.Models;

namespace PlayTogether.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly ISimpleCRUDService _crudService;
        private readonly IPasswordHasher _passwordHasher;
        private readonly JWTTokenProvider _jwtTokenProvider;

        public AuthController(ISimpleCRUDService crudService, JWTTokenProvider jwtTokenProvider, IPasswordHasher passwordHasher)
        {
            _crudService = crudService;
            _jwtTokenProvider = jwtTokenProvider;
            _passwordHasher = passwordHasher;
        }

        [Route("[controller]/[action]")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ValidationResultMessages.InvalidModelData);
            }

            var user = await _crudService.Find<User>(u => u.UserName == model.UserName);
            if (user == null)
            {
                user = await _crudService.CreateOrUpdate<User>(new User()
                {
                    UserName = model.UserName,
                    Profile = new Profile(),
                    PasswordHash = _passwordHasher.HashPassword(model.Password)
                });

                return Ok(GetJWTTokens(user));
            }
            else
            {
                var result = _passwordHasher.VerifyHashedPassword(user.PasswordHash, model.Password);
                if (result == PasswordVerificationResult.Failed)
                {
                    return BadRequest(ValidationResultMessages.LoginWrongCredentials);
                }
            }

            return Ok(GetJWTTokens(user));
        }

        private dynamic GetJWTTokens(User user)
        {
            return new
            {
                accessToken = _jwtTokenProvider.GetAccessToken(user),
                userName = user.UserName
            };
        }
    }
}