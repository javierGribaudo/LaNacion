namespace LaNacionChallenge.Services
{
    public interface IContactService<T> : IGenericService<T>
    {
        Task<IEnumerable<T>> GetByCityOrStateAsync(string state, string city);
        Task<IEnumerable<T>> GetByEmailAsync(string email);
        Task<IEnumerable<T>> GetByPhoneAsync(string phone);
    }
}
