namespace Infrastructure.Specifications.Expressions;

using System.Linq.Expressions;

public sealed class WhereExpressionInfo<T>
{
    public WhereExpressionInfo(Expression<Func<T, bool>> filter)
    {
        Filter = filter ?? throw new ArgumentNullException(nameof(filter));
    }

    public Expression<Func<T, bool>> Filter { get; }
}