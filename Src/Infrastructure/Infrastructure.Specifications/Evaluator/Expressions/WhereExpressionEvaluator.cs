namespace Infrastructure.Specifications.Evaluator.Expressions;

using Abstractions;

public sealed class WhereExpressionEvaluator : IExpressionEvaluator
{
    public static WhereExpressionEvaluator Instance { get; } = new();

    public IQueryable<T> GetQuery<T>(IQueryable<T> query,
        ISpecification<T> specification)
        where T : class
    {
        return specification.WhereExpressions.Aggregate(query, (current,
            info) => current.Where(info.Filter));
    }
}