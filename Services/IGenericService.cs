namespace LaNacionChallenge.Services
{
    public interface IGenericService<T>
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<bool> AddOrUpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);
    }
}
