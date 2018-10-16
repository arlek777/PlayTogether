using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using PlayTogether.Domain;

namespace PlayTogether.Web.Infrastructure
{
    public class WebSession
    {
        private Guid _userId = Guid.Empty;
        private UserType _userType = UserType.Uknown;
        private readonly IHttpContextAccessor _httpContext;
        private readonly JWTTokenProvider _jwtTokenProvider;
        public Guid UserId
        {
            get
            {
                if (_userId == Guid.Empty)
                {
                    Init();
                }

                return _userId;
            }
        }

        public UserType UserType
        {
            get
            {
                if (_userType == UserType.Uknown)
                {
                    Init();
                }

                return _userType;
            }
        }

        public WebSession(IHttpContextAccessor httpContext, JWTTokenProvider jwtTokenProvider)
        {
            _httpContext = httpContext;
            _jwtTokenProvider = jwtTokenProvider;
        }

        public void Logout()
        {
            _userId = Guid.Empty;
            _userType = UserType.Uknown;
        }

        private void Init()
        {
            var headers = _httpContext.HttpContext.Request.Headers;
            var tokenStringHeader = headers["Authorization"];
            var token = tokenStringHeader[0]?.Split(' ')[1];
            var decodedAccessToken = _jwtTokenProvider.GetDecodedAccessToken(token);

            _userId = Guid.Parse(decodedAccessToken["sub"].ToString());
            var roles = decodedAccessToken["roles"] as JArray;

            UserType userType;
            if (Enum.TryParse(roles[0].Value<string>(), out userType))
            {
                _userType = userType;
            }
        }
    }
}