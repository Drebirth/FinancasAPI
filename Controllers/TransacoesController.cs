using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projeto_FinancasAPI.Context;
using Projeto_FinancasAPI.Models;
using Projeto_FinancasAPI.Services.Transacoes;


namespace Projeto_FinancasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class TransacoesController : ControllerBase
    {

        private readonly TransacoesService _services;

        public TransacoesController(TransacoesService services)
        {
            _services = services;
        }

        [HttpGet(Name ="GetTransacao")]
        public async Task<IActionResult> Get()
        {
            var transacoes = await _services.GetAllAsync();
            return Ok(transacoes);  

        }

        [HttpPost]
        public async Task<IActionResult> Post(Transacao transacao)
        {
             
            await _services.CreateTransacaoAsync(transacao);
            return new CreatedAtRouteResult("GetTransacao", new {id = transacao.Id}, transacao);
            
        }
    }
}
