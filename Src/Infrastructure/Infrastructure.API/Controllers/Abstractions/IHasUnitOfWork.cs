namespace Infrastructure.API.Controllers.Abstractions;

using Persistence.UnitOfWorks;

public interface IHasUnitOfWork
{
    IUnitOfWork UnitOfWork { get; }
}