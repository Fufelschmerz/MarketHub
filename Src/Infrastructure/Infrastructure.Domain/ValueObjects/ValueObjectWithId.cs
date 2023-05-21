namespace Infrastructure.Domain.ValueObjects;

public abstract class ValueObjectWithId : IValueObjectWithId
{
    public virtual long Id { get;  init; }
}