using Projeto_FinancasAPI.Models;

namespace Projeto_FinancasAPI.Repository.Contas
{
    public interface IContaRepository: IRepository<Conta> 
    {
        Task<string> GetUsuarioExiste(string usuario);
    }
}
