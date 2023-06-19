namespace MarketHub.Application.Services.Commands.Handlers;

using global::Infrastructure.Application.Services.Commands;
using global::Infrastructure.Domain.Entities;
using global::Infrastructure.Persistence.Repositories;

public sealed class CreateEntityCommandHandler<TEntity> : ICommandHandler<CreateEntityCommand<TEntity>>
    where TEntity : class, IEntity
{
    private readonly IRepository<TEntity> _repository;

    public CreateEntityCommandHandler(IRepository<TEntity> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public Task HandleAsync(CreateEntityCommand<TEntity> command,
        CancellationToken cancellationToken = default)
    {
        return _repository.AddAsync(command.Entity,
            cancellationToken);
    }
}