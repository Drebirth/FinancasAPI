using Microsoft.EntityFrameworkCore;
using Projeto_FinancasAPI.Context;
using Projeto_FinancasAPI.Models;

namespace Projeto_FinancasAPI.Repository.Contas
{
    public class ContaRepository : Repository<Conta>, IContaRepository
    {
        public ContaRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<string> GetUsuarioExiste(string usuario)
        {
            var usuarioExiste = await _context.Contas
                .Where(c => c.Usuario == usuario)
                .Select(c => c.Usuario)
                .FirstOrDefaultAsync();

            return usuarioExiste;
        }
    }
}
