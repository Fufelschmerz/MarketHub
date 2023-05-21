namespace Infrastructure.Specifications.Expressions;

using System.Linq.Expressions;

public sealed class IncludeExpressionInfo<T>
{
    public IncludeExpressionInfo(Expression<Func<T, object>> includeExceptions)
    {
        IncludeExceptions = includeExceptions ?? throw new ArgumentNullException(nameof(includeExceptions));
    }

    public Expression<Func<T, object>> IncludeExceptions { get; }
}