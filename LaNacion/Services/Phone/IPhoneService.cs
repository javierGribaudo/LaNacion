namespace LaNacionChallenge.Services
{
    public interface IPhoneService<T>:IGenericService<T>
    {
        Task<IEnumerable<T>> GetByContactIdAsync(int id);
    }
}
