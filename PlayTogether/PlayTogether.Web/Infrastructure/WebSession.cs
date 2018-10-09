using System;
using Microsoft.AspNetCore.Http;

namespace PlayTogether.Web.Infrastructure
{
    public class WebSession
    {
        private Guid _userId = Guid.Empty;
        private readonly IHttpContextAccessor _httpContext;
        private readonly JWTTokenProvider _jwtTokenProvider;
        public Guid UserId
        {
            get
            {
                if (_userId == Guid.Empty)
                {
                    var headers = _httpContext.HttpContext.Request.Headers;
                    var tokenStringHeader = headers["Authorization"];
                    var token = tokenStringHeader[0]?.Split(' ')[1];
                    var decodedAccessToken = _jwtTokenProvider.GetDecodedAccessToken(token);

                    _userId = Guid.Parse(decodedAccessToken["sub"].ToString());
                }

                return _userId;
            }
        }

        public WebSession(IHttpContextAccessor httpContext, JWTTokenProvider jwtTokenProvider)
        {
            _httpContext = httpContext;
            _jwtTokenProvider = jwtTokenProvider;
        }
    }
}