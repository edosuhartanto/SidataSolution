// ******************************************************
// Copyright (c) 2026 Sidata Solusi Ritel
// Licensed under the MIT License.
// build by Edo Suhartanto
// ******************************************************
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Sidata.Auth.Data;

namespace Sidata.Abstractions.Auth.JWT.Services
{
    public class JwtTokenService(IConfiguration config) : IJwtTokenService
    {
        private readonly IConfiguration _config = config;

        private string GenerateToken(IEnumerable<Claim> claims, int expiryMinutes)
        {
            var secret = _config["Jwt:Secret"]!;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateAccessToken(ApiClient client)
        {
            var claims = new List<Claim>
            {
                new ("token_type", "access"),
                new ("client_code", client.ClientCode),
                new (JwtRegisteredClaimNames.Sub, client.ClientCode),
                new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var minutes = int.Parse(_config["Jwt:AccessTokenExpiryMinutes"]!);
            return GenerateToken(claims, minutes);
        }

        public string GenerateLoginToken(AppUser user)
        {
            var claims = new List<Claim>
            {
                new ("token_type", "login"),
                new (ClaimTypes.NameIdentifier, user.Id.ToString()),
                new (ClaimTypes.Name, user.Username),
                new (JwtRegisteredClaimNames.Sub, user.Username),
                new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var minutes = int.Parse(_config["Jwt:LoginTokenExpiryMinutes"]!);
            return GenerateToken(claims, minutes);
        }
    }
}