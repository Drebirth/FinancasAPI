using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projeto_FinancasAPI.Context;
using Projeto_FinancasAPI.Models;

namespace Projeto_FinancasAPI.Repository.Categorias
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        
        public CategoriaRepository(AppDbContext context) : base(context)
        {
            
        }

        public Task<Categoria> GetCategoriaAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
