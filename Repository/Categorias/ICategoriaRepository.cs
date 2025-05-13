using Projeto_FinancasAPI.Models;

namespace Projeto_FinancasAPI.Repository.Categorias
{
    public interface ICategoriaRepository
    {
        Task<IEnumerable<Categoria>> GetAllAsync();
        Task<Categoria> GetCategoriaAsync(int id);
        Task<Categoria> AddCategoriaAsync(Categoria categoria);
        Task<Categoria> UpdateCategoriaAsync(Categoria categoria);
        Task<Categoria> DeleteCategoriaAsync(int id);

    }
}
