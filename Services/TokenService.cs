﻿using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Projeto_FinancasAPI.Services
{
    public class TokenService : ITokenService
    {
        public string GenerateRefreshToken()
        {
           var secureRandomBytes = new byte[128];

            using var randomNumberGenerator = RandomNumberGenerator.Create();

            randomNumberGenerator.GetBytes(secureRandomBytes);

            var refreshToken = Convert.ToBase64String(secureRandomBytes);

            return refreshToken;
        }

        public JwtSecurityToken GenerateToken(IEnumerable<Claim> claims, IConfiguration _configuration)
        {
            var key = _configuration.GetSection("JWT").GetValue<string>("SecretKey") ??
                throw new InvalidOperationException("Secretkey Invalida! ");

            var privateKey = Encoding.UTF8.GetBytes(key);

            var signingCredentials = new SigningCredentials( new SymmetricSecurityKey(privateKey),
                SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_configuration.GetSection("JWT").GetValue<double>("TokenValidityInMinutes")),
                SigningCredentials = signingCredentials,
                Issuer = _configuration.GetSection("JWT").GetValue<string>("ValidIssuer"),
                Audience = _configuration.GetSection("JWT").GetValue<string>("ValidAudience")
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            return token;
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token, IConfiguration _configuration)
        {
            var secretKey = _configuration["JWT:SecretKey"] ?? throw new ArgumentException("Invalid Secret Key!");

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                ValidateLifetime = false, // Não valida o tempo de expiração
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

            if(securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }
    }
}
