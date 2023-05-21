namespace Infrastructure.Persistence.UnitOfWorks;

using System.Data;

public interface IUnitOfWork : IDisposable
{
    bool Disposed { get; }

    IDbTransaction? Transaction { get; }

    Task BeginTransaction(CancellationToken cancellationToken = default);

    void RollBack();

    void Commit();
}