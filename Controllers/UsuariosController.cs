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

namespace Projeto_FinancasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly ContaService _service;
        public UsuariosController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,ContaService service)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _service = service;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(Conta login)
        {
         var user =  userManager.FindByEmailAsync(login.Email).Result;
         if (user != null)
         {
             var result = signInManager.PasswordSignInAsync(user, login.Senha, false, false).Result;
             if (result.Succeeded)
             {
                 return Ok("Login realizado com sucesso!");
             }
             else
             {
                 return BadRequest("Senha incorreta.");
             }
         }
         else
         {
             return BadRequest("Usuário não encontrado.");
         }
           

        }

        [HttpPost("Registar")]
        public async Task<IActionResult> Register(Conta register)
        {
            var user = new IdentityUser
            {
                UserName = register.Usuario,
                Email = register.Email
            };
            var result = await userManager.CreateAsync(user, register.Senha);
            if (result.Succeeded)
            {
                // Criptografar a senha
                using (var hmac = new HMACSHA512())
                {
                    register.Senha = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(register.Senha)));
                }
                await _service.CreateContaAsync(register);
                return Ok("Usuário registrado com sucesso!");
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }
    }
}
