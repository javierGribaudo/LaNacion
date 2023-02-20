using LaNacionChallenge.Context;
using LaNacionChallenge.Repository;
using System.Data;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly IDbConnection _db;
    private IDbTransaction _transaction;
    private bool _disposed;
    private bool _isCommitted;
    public UnitOfWork(IDbConnection db)
    {
        _db = db;
        if (_db.State != ConnectionState.Open)
        {
            _db.Open();
        }
        if (_transaction == null)
        {
            _transaction = _db.BeginTransaction();
        }
    }

    public async Task<int> SaveChangesAsync()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(UnitOfWork));
        }

        _transaction.Commit();
        _isCommitted = true;
        _transaction = _db.BeginTransaction();
        return await Task.FromResult(0);
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            _transaction.Dispose();
            _disposed = true;
        }
    }
    public bool IsCommitted()
    {
        return _isCommitted;
    }
}