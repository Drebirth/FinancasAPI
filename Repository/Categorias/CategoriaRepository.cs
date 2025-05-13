using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projeto_FinancasAPI.Context;
using Projeto_FinancasAPI.Models;

namespace Projeto_FinancasAPI.Repository.Categorias
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;

        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Categoria> AddCategoriaAsync(Categoria categoria)
        {
            await _context.Categorias.AddAsync(categoria);
            await _context.SaveChangesAsync();
            return categoria;
        }

        public async Task<Categoria> DeleteCategoriaAsync(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria != null)
            {
                _context.Categorias.Remove(categoria);
                await _context.SaveChangesAsync();

            }

            return  categoria;

        }

        public async Task<IEnumerable<Categoria>> GetAllAsync()
        {
            var categorias = await _context.Categorias.ToListAsync();
            return categorias;
        }

        public async Task<Categoria> GetCategoriaAsync(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);

            if (categoria == null)
            {
                throw new KeyNotFoundException("Categoria não encontrada, favor tentar novamente!");
            }
            return categoria;
        }

        public async Task<Categoria> UpdateCategoriaAsync(Categoria categoria)
        {
            //var categoriaDB = await _context.Categorias.FindAsync(categoria.Id);

            //if (categoriaDB == null)
            //{
            //    throw new ArgumentException("Categoria não encontrada, favor tentar novamente!");
            //}

            _context.Entry(categoria).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return categoria;
        }
    }
}
