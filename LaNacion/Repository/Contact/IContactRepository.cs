namespace LaNacionChallenge.Repository
{
    public interface IContactRepository<T>: IGenericRepository<T>
    {
        Task<IEnumerable<T>> GetByCityOrStateAsync(string state, string city);
        Task<IEnumerable<T>> SearchAsync(string email, string phone);
        Task<IEnumerable<T>> GetByEmailAsync(string email);
        Task<IEnumerable<T>> GetByPhoneAsync(string phone);
    }
}
