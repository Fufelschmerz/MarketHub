namespace MarketHub.Application.Services.Commands.Handlers;

using global::Infrastructure.Application.Services.Commands;
using global::Infrastructure.Domain.Entities;
using global::Infrastructure.Persistence.Repositories;

public sealed class CreateEntityCommandHandler<TEntity> : ICommandHandler<CreateEntityCommand<TEntity>>
    where TEntity : class, IEntity
{
    private readonly IDbRepository<TEntity> _dbRepository;

    public CreateEntityCommandHandler(IDbRepository<TEntity> dbRepository)
    {
        _dbRepository = dbRepository ?? throw new ArgumentNullException(nameof(dbRepository));
    }

    public Task HandleAsync(CreateEntityCommand<TEntity> command,
        CancellationToken cancellationToken = default)
    {
        return _dbRepository.AddAsync(command.Entity,
            cancellationToken);
    }
}