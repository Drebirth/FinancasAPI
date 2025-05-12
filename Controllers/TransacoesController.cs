using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projeto_FinancasAPI.Context;
using Projeto_FinancasAPI.Models;

namespace Projeto_FinancasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransacoesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TransacoesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet(Name ="GetTransacao")]
        public async Task<IActionResult> Get()
        {
            var transacoes = await _context.Transacoes.ToListAsync();
            return Ok(transacoes);  

        }

        [HttpPost]
        public async Task<IActionResult> Post(Transacao transacao)
        {
             _context.Transacoes.Add(transacao);
            await _context.SaveChangesAsync();
            return new CreatedAtRouteResult("GetTransacao", new {id = transacao.Id}, transacao);
            //return new CreatedAtRouteResult("GetCategoria", new { id = categoria.Id }, categoria);
        }
    }
}
