using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Projeto_FinancasAPI.Services
{
    public interface ITokenService
    {
        JwtSecurityToken GenerateToken(IEnumerable<Claim> claims, IConfiguration _configuration);

        string GenerateRefreshToken();

        ClaimsPrincipal GetPrincipalFromExpiredToken(string token, IConfiguration _configuration);
    }
}
