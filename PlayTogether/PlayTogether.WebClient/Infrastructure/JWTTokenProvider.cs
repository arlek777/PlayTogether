﻿using System;
using System.Collections.Generic;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Microsoft.Extensions.Options;
using PlayTogether.BusinessLogic;
using PlayTogether.Domain;

namespace PlayTogether.WebClient.Infrastructure
{
    public class JWTTokenProvider
    {
        private readonly JWTSettings _jwtSettings;

        public JWTTokenProvider(IOptions<JWTSettings> optionsAccessor)
        {
            _jwtSettings = optionsAccessor.Value;
        }

        public string GetIdToken(User user)
        {

            var payload = new Dictionary<string, object>
            {
                {"id", user.Id},
                {"username", user.UserName}
            };
            return GetToken(payload);
        }

        public string GetAccessToken(User user)
        {
            var payload = new Dictionary<string, object>
            {
                { "sub", user.Id },
                { "email", user.UserName }
            };
            return GetToken(payload);
        }

        private string GetToken(Dictionary<string, object> payload)
        {
            var secret = _jwtSettings.SecretKey;

            payload.Add("iss", _jwtSettings.Issuer);
            payload.Add("aud", _jwtSettings.Audience);
            payload.Add("iat", DateTime.Now.ConvertToUnixTimestamp());
            payload.Add("exp", DateTime.Now.AddYears(1).ConvertToUnixTimestamp());

            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            return encoder.Encode(payload, secret);
        }
    }
}
