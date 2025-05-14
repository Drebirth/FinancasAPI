namespace Projeto_FinancasAPI.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(int id);
        //Task<T> GetAsync(T entity);
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);

    }
}
