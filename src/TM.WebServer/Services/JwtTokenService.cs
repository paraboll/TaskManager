using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using TM.WebServer.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace TM.WebServer.Services
{
    public interface IJwtTokenService
    {
        string GetJwtToken(List<Claim> userClaims);
        int GetTokenTime();
    }

    public class JwtTokenService : IJwtTokenService
    {
        private readonly ILogger<JwtTokenService> _logger;
        private readonly JwtToken _jwtConfig;

        public JwtTokenService(ILogger<JwtTokenService> logger, IOptions<JwtToken> jwtConfig)
        {
            _logger = logger;

            _jwtConfig = jwtConfig.Value;
        }

        public string GetJwtToken(List<Claim> userClaims)
        {
            _logger.LogDebug($"Вызван метод GetJwtToken для {string.Join(",", userClaims.Select(_ => _.Value))}");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtConfig.ValidIssuer,
                audience: _jwtConfig.ValidAudience,
                claims: userClaims,
                expires: DateTime.Now.AddMinutes(_jwtConfig.ExpiryInMinutes),
                signingCredentials: creds);

            var handler = new JwtSecurityTokenHandler();
            return handler.WriteToken(token);
        }

        public int GetTokenTime()
        {
            return _jwtConfig.ExpiryInMinutes;
        }
    }
}
