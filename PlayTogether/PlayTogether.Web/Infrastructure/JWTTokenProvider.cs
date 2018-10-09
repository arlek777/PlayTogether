using System;
using System.Collections.Generic;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Microsoft.Extensions.Options;
using PlayTogether.BusinessLogic;
using PlayTogether.Domain;
using PlayTogether.Web.Models;

namespace PlayTogether.Web.Infrastructure
{
    public class JWTTokenProvider
    {
        private readonly JWTSettings _jwtSettings;
        private readonly IJwtEncoder _encoder;
        private readonly IJwtDecoder _decoder;

        public JWTTokenProvider(IOptions<JWTSettings> optionsAccessor)
        {
            _jwtSettings = optionsAccessor.Value;

            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtValidator validator = new JwtValidator(serializer, new UtcDateTimeProvider());

            _encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
            _decoder = new JwtDecoder(serializer, validator, urlEncoder);
        }

        public string CreateEncodedAccessToken(User user)
        {
            var payload = new Dictionary<string, object>
            {
                { "sub", user.Id },
                { "email", user.UserName },
                { "roles", new [] { user.Type.ToString() } }
            };
            return CreateJwtEncodedToken(payload);
        }

        public IDictionary<string, object> GetDecodedAccessToken(string token)
        {
            return _decoder.DecodeToObject(token, _jwtSettings.SecretKey, true);
        }

        private string CreateJwtEncodedToken(Dictionary<string, object> payload)
        {
            var secret = _jwtSettings.SecretKey;

            payload.Add("iss", _jwtSettings.Issuer);
            payload.Add("aud", _jwtSettings.Audience);
            payload.Add("iat", DateTime.Now.ConvertToUnixTimestamp());
            payload.Add("exp", DateTime.Now.AddYears(1).ConvertToUnixTimestamp());

            return _encoder.Encode(payload, secret);
        }
    }
}
