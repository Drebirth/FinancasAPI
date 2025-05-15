using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projeto_FinancasAPI.Context;
using Projeto_FinancasAPI.Models;
using Projeto_FinancasAPI.Repository.Categorias;
using Projeto_FinancasAPI.Services.Categorias;

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
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}", Name = "GetCategoria")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _service.GetCategoriaAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post(Categoria categoria)
        {
            
            await _service.CreateCategoriaAsync(categoria);
            return new CreatedAtRouteResult("GetCategoria", new { id = categoria.Id }, categoria);
        }

        [HttpPut("{id}", Name = "PutCategoria")]
        public async Task<IActionResult> Put(int id, Categoria categoria)
        {
            await _service.UpdateCategoriaAsync(id, categoria);
            return Ok("Categoria atualizada com sucesso!");
        }

        [HttpDelete("{id}", Name = "DeleteCategoria")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteCategoriaAsync(id);
            return Ok("Categoria removida com sucesso!");
        }
    }
}
