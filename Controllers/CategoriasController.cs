using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projeto_FinancasAPI.Context;
using Projeto_FinancasAPI.Models;
using Projeto_FinancasAPI.Repository.Categorias;
using Projeto_FinancasAPI.Services.Categoria;

namespace Projeto_FinancasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        //private readonly AppDbContext _context;
        private readonly ICategoriaRepository _repository;
        private readonly CategoriaService _service;

        public CategoriasController(ICategoriaRepository repository, CategoriaService service)
        {
            _repository = repository;
            _service = service;
        }

        [HttpGet(Name = "GetCategorias")]
        public async Task<IActionResult> Get()
        {
            //var categorias = await _context.Categorias.ToListAsync();
            //var categorias = await _repository.GetAllAsync();
            //await _service.GetAllAsync();
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}", Name = "GetCategoria")]
        public async Task<IActionResult> Get(int id)
        {
            //var categoria = await _context.Categorias.FindAsync(id);
            //var categoria =  await _repository.GetAsync(id);
            //if (categoria == null)
            //{
            //    return NotFound("Categoria não encontrada, favor tentar novamente!");
            //}
            return Ok(await _service.GetCategoriaAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post(Categoria categoria)
        {
            //if (categoria is null)
            //{
            //    return BadRequest();
            //}
            //_context.Categorias.Add(categoria);
            //await _context.SaveChangesAsync();
            //await _repository.CreateAsync(categoria);
            
            await _service.CreateCategoriaAsync(categoria);
            return new CreatedAtRouteResult("GetCategoria", new { id = categoria.Id }, categoria);
        }

        [HttpPut("{id}", Name = "PutCategoria")]
        public async Task<IActionResult> Put(int id, Categoria categoria)
        {
            //if (id != categoria.Id)
            //{
            //    return BadRequest();
            //}
            //if (!await _context.Categorias.AnyAsync(x => x.Id == id))
            //{
            //    return NotFound("Categoria não encontrada, favor tentar novamente!");
            //}
            //_context.Entry(categoria).State = EntityState.Modified;
            //await _context.SaveChangesAsync();
          
            //await _repository.UpdateAsync(categoria);
            await _service.UpdateCategoriaAsync(id, categoria);
            return Ok("Categoria atualizada com sucesso!");
        }

        [HttpDelete("{id}", Name = "DeleteCategoria")]
        public async Task<IActionResult> Delete(int id)
        {
            //var categoria = await _context.Categorias.FindAsync(id);
            //if (categoria == null)
            //{
            //    return NotFound("Categoria não encontrada, favor tentar novamente!");
            //}
            //_context.Categorias.Remove(categoria);
            //await _context.SaveChangesAsync();
            //var categoria = await _repository.GetAsync(id);
            //return Ok("Categoria removida com sucesso!");
            //await _repository.DeleteAsync(categoria);
            await _service.DeleteCategoriaAsync(id);
            return Ok("Categoria removida com sucesso!");
        }
    }
}
