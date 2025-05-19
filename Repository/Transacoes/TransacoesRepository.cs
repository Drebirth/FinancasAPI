using Microsoft.EntityFrameworkCore;
using Projeto_FinancasAPI.Context;
using Projeto_FinancasAPI.Models;

namespace Projeto_FinancasAPI.Repository.Transacoes
{
    public class TransacoesRepository : Repository<Transacao>, ITransacoesRepository
    {
        public TransacoesRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Transacao>> GetAllFindByIdUser(int id)
        {
            //var transacoes = _context.Transacoes.ToList();

            //var transacoesUsuario = await transacoes.Where(t => t.ContaId == id).ToListAsync();
            //return transacoesUsuario;

            var transacoes = await _context.Transacoes                
                .Where(t => t.ContaId == id)
                .ToListAsync();

            return transacoes;
        }
    }
    
}
