using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projeto_FinancasAPI.Context;
using Projeto_FinancasAPI.Models;
using Projeto_FinancasAPI.Services.Categorias;
using Projeto_FinancasAPI.Services.Contas;

namespace Projeto_FinancasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContasController : ControllerBase
    {
        //private readonly AppDbContext _context;
        private readonly ContaService _service;

        public ContasController(ContaService service)
        {
            _service = service;
        }

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

        //[HttpPut("{id}", Name = "PutConta")]
        //public async Task<IActionResult> Put(int id, Conta conta)
        //{
        //    if (id != conta.Id)
        //    {
        //        return BadRequest();
        //    }

        //    if (!await _context.Contas.AnyAsync(x => x.Id == id))
        //    {
        //        return NotFound("Conta não encontrada, favor tentar novamente!");
        //    }

        //    _context.Entry(conta).State = EntityState.Modified;
        //    await _context.SaveChangesAsync();
        //    return Ok("Conta atualizada com sucesso!");
        //}

        //[HttpDelete("{id}", Name = "DeleteConta")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var conta = await _context.Contas.FindAsync(id);
        //    if (conta == null)
        //    {
        //        return NotFound("Conta não encontrada, favor tentar novamente!");
        //    }
        //    _context.Contas.Remove(conta);
        //    await _context.SaveChangesAsync();
        //    return Ok("Conta excluída com sucesso!");
        //}
    }
}
