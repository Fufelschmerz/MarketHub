namespace MarketHub.Application.Services.Commands.Handlers;

using global::Infrastructure.Application.Services.Commands;
using global::Infrastructure.Domain.Entities;
using global::Infrastructure.Persistence.Repositories;

public sealed class DeleteEntityCommandHandler<TEntity> : ICommandHandler<DeleteEntityCommand<TEntity>>
    where TEntity : class, IEntity
{
    private readonly IRepository<TEntity> _repository;

    public DeleteEntityCommandHandler(IRepository<TEntity> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public Task HandleAsync(DeleteEntityCommand<TEntity> command,
        CancellationToken cancellationToken = default)
    {
        return _repository.DeleteAsync(command.Entity,
            cancellationToken);
    }
}