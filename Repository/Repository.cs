using Microsoft.EntityFrameworkCore;
using Projeto_FinancasAPI.Context;
using Projeto_FinancasAPI.Models;

namespace Projeto_FinancasAPI.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            //return entity;
        }

        public async Task DeleteAsync(T entity)
        {
              _context.Set<T>().Remove(entity);
              await _context.SaveChangesAsync();

        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetAsync(int id)
        {
           return await _context.Set<T>().FindAsync(id);
        }

      

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Entry(entity).State = EntityState.Modified;
            //_context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
