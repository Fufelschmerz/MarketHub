namespace Infrastructure.Application.Exceptions.Attributes;

[AttributeUsage(AttributeTargets.Field)]
public sealed class MapDomainExceptionAttribute : Attribute
{
    public MapDomainExceptionAttribute(Type domainExceptionType)
    {
        DomainExceptionType = domainExceptionType ?? throw new ArgumentNullException(nameof(domainExceptionType));
    }
    
    public Type DomainExceptionType { get; }
}