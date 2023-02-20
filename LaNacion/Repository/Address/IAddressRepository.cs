namespace LaNacionChallenge.Repository
{
    public interface IAddressRepository<T>: IGenericRepository<T>
    {
        Task<IEnumerable<T>> GetByContactIdAsync(int id);
    }
}
