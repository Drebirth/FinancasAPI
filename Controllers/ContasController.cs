using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Projeto_FinancasAPI.Models;
using Projeto_FinancasAPI.Services.Contas;

namespace Projeto_FinancasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ContasController : ControllerBase
    {
        //private readonly AppDbContext _context;
        private readonly ContaService _service;

        public ContasController(ContaService service)
        {
            _service = service;
        }



        [Authorize]
        [HttpGet(Name = "GetContas")]
        public async Task<IActionResult> Get()
        {
            //var contas = await _context.Contas.ToListAsync();
            
            return Ok(await _service.GetContasAsync());
        }

        [HttpPost(Name = "PostConta")]
        public async Task<IActionResult> Post(Conta conta)
        {

            if (conta is null)
            {
                return BadRequest();
            }

            //_context.Contas.Add(conta);
            //await _context.SaveChangesAsync();
            await _service.CreateContaAsync(conta);
            
            return new CreatedAtRouteResult("GetContas", new { id = conta.Id }, conta);

        }

        [HttpPut("{id}", Name = "PutConta")]
        public async Task<IActionResult> Put(int id, Conta conta)
        {
            await _service.UpdateContaAsync(id, conta);

            return Ok("Dados atualizados");
        }

        [HttpDelete("{id}", Name = "DeleteConta")]
        public async Task<IActionResult> Delete(int id)
        {
            
            await _service.DeleteContaAsync(id);
            return Content("Conta excluída com sucesso!");
        }
    }
}
