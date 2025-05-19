using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Projeto_FinancasAPI.Models;
using System.Security.Claims;
using Projeto_FinancasAPI.Services.Contas;
using System.Security.Cryptography;
using System.Text;
using Projeto_FinancasAPI.DTOs;
using Projeto_FinancasAPI.Services;
using System.IdentityModel.Tokens.Jwt;

namespace Projeto_FinancasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<AplicationUser> userManager;
        //private readonly SignInManager<IdentityUser> signInManager;
        private readonly IConfiguration _configuration;
        private readonly ContaService _service;

        public AuthController(UserManager<AplicationUser> userManager, 
           //SignInManager<IdentityUser> signInManager,
            ContaService service,
            ITokenService tokenService,
            IConfiguration configuration)
        {
            _configuration = configuration;
            this.userManager = userManager;
            //this.signInManager = signInManager;
            _tokenService = tokenService;
            _service = service;
        }

        //[HttpPost("Login")]
        //public async Task<IActionResult> Login(LoginDTO login)
        //{
        // var user =  userManager.FindByEmailAsync(login.Email).Result;
        // if (user != null)
        // {
        //     var result = signInManager.PasswordSignInAsync(user, login.Senha, false, false).Result;
        //     if (result.Succeeded)
        //     {
        //         return Ok("Login realizado com sucesso!");
        //     }
        //     else
        //     {
        //         return BadRequest("Senha incorreta.");
        //     }
        // }
        // else
        // {
        //     return BadRequest("Usuário não encontrado.");
        // }


        //}

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            var user = await userManager.FindByEmailAsync(login.Email);
            if (user is not null && await userManager.CheckPasswordAsync(user, login.Senha))
            {
                var userRoles = await userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = _tokenService.GenerateToken(authClaims, _configuration);

                var refreshToken = _tokenService.GenerateRefreshToken();

                _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInMinutes"], out var refreshTokenValidityInMinutes);

                user.RefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(refreshTokenValidityInMinutes);

                user.RefreshTokem = refreshToken;

                await userManager.UpdateAsync(user);

                return Ok(new
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    refreshTokem = refreshToken,
                    //refreshTokenExpiration = DateTime.UtcNow.AddMinutes(refreshTokenValidityInMinutes)
                });
            }

                return Unauthorized();
        }

        [HttpPost("Cadastro")]
        public async Task<IActionResult> Register([FromBody] CadastroDTO cadastro)
        {
            var userExists = await userManager.FindByEmailAsync(cadastro.Email);

            if (userExists is not null)
            {
                return BadRequest("Usuário já existe!");
            }

            AplicationUser user = new()
            {
                Email = cadastro.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = cadastro.Usuario
            };

            var result = await userManager.CreateAsync(user, cadastro.Senha);
            await _service.CreateContaAsync(new Conta
            {
                Usuario = cadastro.Usuario,
                Email = cadastro.Email,
                Senha = cadastro.Senha,
                Saldo = 0
            });
            if (result.Errors.Any())
            {
                return BadRequest(result.Errors);
            }
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao cadastrar usuário!");
            }

            return Ok(new ResponseDTO { Message = "Usuário cadastrado com sucesso!", Status = "Sucesso!" });  

        }


        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(TokenDTO tokenModel)
        {
            if (tokenModel is null)
            {
                return BadRequest("Invalid client request");
            }

            string? accessToken = tokenModel.AcessToken
                                    ?? throw new ArgumentNullException(nameof(tokenModel));

            string? refreshToken = tokenModel.RefreshToken
                                          ?? throw new ArgumentNullException(nameof(tokenModel));

            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken!, _configuration);

            if (principal == null)
            {
                return BadRequest("Invalid access token/refresh token");
            }

            string username = principal.Identity.Name;

            var user = await userManager.FindByNameAsync(username!);

            if (user == null || user.RefreshTokem != refreshToken
                || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return BadRequest("Invalid access token/refresh token");
            }

            var newAccessToken = _tokenService.GenerateToken(
                principal.Claims.ToList(), _configuration);

            var newRefreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshTokem = newRefreshToken;
            await userManager.UpdateAsync(user);

            return new ObjectResult(new
            {
                accessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                refreshToken = newRefreshToken,
            });
        }

        //[HttpPost("Registar")]
        //public async Task<IActionResult> Register(Conta register)
        //{
        //    var user = new IdentityUser
        //    {
        //        UserName = register.Usuario,
        //        Email = register.Email
        //    };
        //    var result = await userManager.CreateAsync(user, register.Senha);
        //    if (result.Succeeded)
        //    {
        //        // Criptografar a senha
        //        using (var hmac = new HMACSHA512())
        //        {
        //            register.Senha = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(register.Senha)));
        //        }
        //        await _service.CreateContaAsync(register);
        //        return Ok("Usuário registrado com sucesso!");
        //    }
        //    else
        //    {
        //        return BadRequest(result.Errors);
        //    }
        //}
    }
}
