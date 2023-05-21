namespace MarketHub.Domain.Abstractions;

public interface ICanBeDeleted
{
    bool IsDeleted { get; }
}