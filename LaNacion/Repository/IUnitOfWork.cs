using LaNacionChallenge.Domain;
using LaNacionChallenge.Repository;

namespace LaNacionChallenge.Repository
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
        bool IsCommitted();
    }
}
