namespace MarketHub.Application.Services.Commands.Handlers;

using global::Infrastructure.Application.Services.Commands;
using global::Infrastructure.Domain.Entities;
using global::Infrastructure.Persistence.Repositories;

public sealed class UpdateEntityCommandHandler<TEntity> : ICommandHandler<UpdateEntityCommand<TEntity>>
    where TEntity : class, IEntity
{
    private readonly IDbRepository<TEntity> _dbRepository;

    public UpdateEntityCommandHandler(IDbRepository<TEntity> dbRepository)
    {
        _dbRepository = dbRepository ?? throw new ArgumentNullException(nameof(dbRepository));
    }

    public Task HandleAsync(UpdateEntityCommand<TEntity> command,
        CancellationToken cancellationToken = default)
    {
        return _dbRepository.UpdateAsync(command.Entity,
            cancellationToken);
    }
}