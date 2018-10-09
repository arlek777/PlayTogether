using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using PlayTogether.BusinessLogic;
using PlayTogether.Domain;
using PlayTogether.Web.Infrastructure;
using PlayTogether.Web.Models;

namespace PlayTogether.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly ISimpleCRUDService _crudService;
        private readonly IPasswordHasher _passwordHasher;
        private readonly JWTTokenProvider _jwtTokenProvider;
        private readonly WebSession _webSession;

        public AuthController(ISimpleCRUDService crudService, JWTTokenProvider jwtTokenProvider, IPasswordHasher passwordHasher, WebSession webSession)
        {
            _crudService = crudService;
            _jwtTokenProvider = jwtTokenProvider;
            _passwordHasher = passwordHasher;
            _webSession = webSession;
        }

        [Route("[controller]/[action]")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ValidationResultMessages.InvalidModelState);
            }

            var user = await _crudService.Find<User>(u => u.UserName == model.UserName);
            if (user == null)
            {
                user = await _crudService.Create<User>(new User()
                {
                    UserName = model.UserName,
                    Type = UserType.Uknown,
                    Profile = new Profile(),
                    PasswordHash = _passwordHasher.HashPassword(model.Password)
                });

                return Ok(GetLoginResponse(user, true));
            }
            else
            {
                var result = _passwordHasher.VerifyHashedPassword(user.PasswordHash, model.Password);
                if (result == PasswordVerificationResult.Failed)
                {
                    return BadRequest(ValidationResultMessages.LoginWrongCredentials);
                }
            }

            return Ok(GetLoginResponse(user, false));
        }

        [Route("[controller]/[action]")]
        [HttpPost]
        public async Task<IActionResult> SelectUserType([FromBody] SelectUserTypeModel model)
        {
            var user = await _crudService.Find<User>(u => u.Id == _webSession.UserId);
            if (user.Type != UserType.Uknown)
            {
                return BadRequest();
            }

            await _crudService.Update<SelectUserTypeModel, User>(_webSession.UserId, model, (to, from) =>
            {
                to.Type = from.UserType;
            });

            return Ok();
        }

        private dynamic GetLoginResponse(User user, bool isNewUser)
        {
            return new
            {
                accessToken = _jwtTokenProvider.CreateEncodedAccessToken(user),
                isNewUser,
                user = new
                {
                    userName = user.UserName,
                    userType = user.Type
                }
            };
        }
    }
}