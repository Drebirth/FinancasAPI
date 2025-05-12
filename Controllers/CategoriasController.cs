using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projeto_FinancasAPI.Context;
using Projeto_FinancasAPI.Models;

namespace Projeto_FinancasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet(Name = "GetCategorias")]
        public async Task<IActionResult> Get()
        {
            var categorias = await _context.Categorias.ToListAsync();
            return Ok(categorias);
        }

        [HttpGet("{id}", Name = "GetCategoria")]
        public async Task<IActionResult> Get(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return NotFound("Categoria não encontrada, favor tentar novamente!");
            }
            return Ok(categoria);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Categoria categoria)
        {
            if (categoria is null)
            {
                return BadRequest();
            }
            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();
            return new CreatedAtRouteResult("GetCategoria", new { id = categoria.Id }, categoria);
        }

        [HttpPut("{id}", Name = "PutCategoria")]
        public async Task<IActionResult> Put(int id, Categoria categoria)
        {
            if (id != categoria.Id)
            {
                return BadRequest();
            }
            if (!await _context.Categorias.AnyAsync(x => x.Id == id))
            {
                return NotFound("Categoria não encontrada, favor tentar novamente!");
            }
            _context.Entry(categoria).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok("Categoria atualizada com sucesso!");
        }

        [HttpDelete("{id}", Name = "DeleteCategoria")]
        public async Task<IActionResult> Delete(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return NotFound("Categoria não encontrada, favor tentar novamente!");
            }
            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();
            return Ok("Categoria removida com sucesso!");
        }
    }
}
