using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Projeto_FinancasAPI.Services;
public class JwtService
{
    private readonly string _chaveSecreta = "zDTbM5JqO3i9ZmwKk4Fi3ZWVQQklxprtoEZSpkBa/qs=";

    public string GerarToken(string usuarioId)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_chaveSecreta));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, usuarioId),
            new Claim(JwtRegisteredClaimNames.Exp, DateTime.UtcNow.AddHours(1).ToString())
        };

        var token = new JwtSecurityToken(
            issuer: "https://localhost:7140",
            audience: "https://localhost:7140",
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}