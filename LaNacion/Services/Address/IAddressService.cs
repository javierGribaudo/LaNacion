namespace LaNacionChallenge.Services
{
    public interface IAddressService<T>: IGenericService<T>
    {
        Task<IEnumerable<T>> GetByContactIdAsync(int id);
    }
}
