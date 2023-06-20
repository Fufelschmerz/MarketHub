namespace MarketHub.Application.Services.Commands.Handlers;

using global::Infrastructure.Application.Services.Commands;
using global::Infrastructure.Domain.Entities;
using global::Infrastructure.Persistence.Repositories;

public sealed class UpdateEntityCommandHandler<TEntity> : ICommandHandler<UpdateEntityCommand<TEntity>>
    where TEntity : class, IEntity
{
    private readonly IRepository<TEntity> _repository;

    public UpdateEntityCommandHandler(IRepository<TEntity> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public Task HandleAsync(UpdateEntityCommand<TEntity> command,
        CancellationToken cancellationToken = default)
    {
        return _repository.UpdateAsync(command.Entity,
            cancellationToken);
    }
}