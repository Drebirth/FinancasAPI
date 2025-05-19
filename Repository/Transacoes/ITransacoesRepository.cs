using Projeto_FinancasAPI.Models;

namespace Projeto_FinancasAPI.Repository.Transacoes
{
    public interface ITransacoesRepository : IRepository<Transacao>
    {
        Task<IEnumerable<Transacao>> GetAllFindByIdUser(int id);
    }
}
