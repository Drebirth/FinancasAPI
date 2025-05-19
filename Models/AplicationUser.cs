using Microsoft.AspNetCore.Identity;

namespace Projeto_FinancasAPI.Models
{
    public class AplicationUser : IdentityUser
    {
        public string? RefreshTokem { get; set; }   
        public DateTime RefreshTokenExpiryTime { get; set; }    
    }
}
