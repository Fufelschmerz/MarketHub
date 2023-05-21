namespace MarketHub.Persistence.UnitOfWorks;

using System.Data;
using Infrastructure.Persistence.UnitOfWorks;
using Microsoft.EntityFrameworkCore.Storage;

public sealed class EfCoreUnitOfWork : IUnitOfWork
{
    private readonly DataContext _dataContext;

    public EfCoreUnitOfWork(DataContext dataContext)
    {
        _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
    }

    public IDbTransaction? Transaction { get; private set; }

    public bool Disposed { get; private set; }

    public async Task BeginTransaction(CancellationToken cancellationToken = default)
    {
        if (Disposed)
            throw new ObjectDisposedException(nameof(EfCoreUnitOfWork));

        IDbContextTransaction transaction = _dataContext.Database.CurrentTransaction ??
                                            await _dataContext.Database.BeginTransactionAsync(cancellationToken);

        Transaction = transaction.GetDbTransaction();
    }

    public void RollBack()
    {
        Transaction?.Rollback();
    }

    public void Commit()
    {
        Transaction?.Commit();
    }

    public void Dispose()
    {
        if (!Disposed)
        {
            _dataContext.Dispose();

            Transaction?.Dispose();
        }

        Disposed = true;
    }
}