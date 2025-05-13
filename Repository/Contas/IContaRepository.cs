using Projeto_FinancasAPI.Models;

namespace Projeto_FinancasAPI.Repository.Contas
{
    public interface IContaRepository
    {
        Task<IEnumerable<Conta>> GetContasAsync();
        Task<Conta> GetContaByIdAsync(int id);
        Task<Conta> AddContaAsync(Conta conta);
        Task<Conta> UpdateContaAsync(Conta conta);
        //Task<bool> DeleteContaAsync(int id);    
        Task<Conta> DeleteContaAsync(int id);
    }
}
