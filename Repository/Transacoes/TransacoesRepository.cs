using Projeto_FinancasAPI.Context;
using Projeto_FinancasAPI.Models;

namespace Projeto_FinancasAPI.Repository.Transacoes
{
    public class TransacoesRepository : Repository<Transacao>, ITransacoesRepository
    {
        public TransacoesRepository(AppDbContext context) : base(context)
        {

        }
    }
    
}
