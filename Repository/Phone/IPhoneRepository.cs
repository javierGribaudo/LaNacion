namespace LaNacionChallenge.Repository
{
    public interface IPhoneRepository<T>: IGenericRepository<T>
    {
        Task<IEnumerable<T>> GetByContactIdAsync(int id);
    }
}
