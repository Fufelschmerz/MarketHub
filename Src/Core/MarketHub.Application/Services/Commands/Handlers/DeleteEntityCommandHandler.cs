namespace MarketHub.Application.Services.Commands.Handlers;

using global::Infrastructure.Application.Services.Commands;
using global::Infrastructure.Domain.Entities;
using global::Infrastructure.Persistence.Repositories;

public sealed class DeleteEntityCommandHandler<TEntity> : ICommandHandler<DeleteEntityCommand<TEntity>>
    where TEntity : class, IEntity
{
    private readonly IDbRepository<TEntity> _dbRepository;

    public DeleteEntityCommandHandler(IDbRepository<TEntity> dbRepository)
    {
        _dbRepository = dbRepository ?? throw new ArgumentNullException(nameof(dbRepository));
    }

    public Task HandleAsync(DeleteEntityCommand<TEntity> command,
        CancellationToken cancellationToken = default)
    {
        return _dbRepository.DeleteAsync(command.Entity,
            cancellationToken);
    }
}