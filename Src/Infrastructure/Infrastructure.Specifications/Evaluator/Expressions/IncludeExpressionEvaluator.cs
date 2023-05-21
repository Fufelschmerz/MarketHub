namespace Infrastructure.Specifications.Evaluator.Expressions;

using Abstractions;
using Microsoft.EntityFrameworkCore;
using Specifications.Expressions;

public sealed class IncludeExpressionEvaluator : IExpressionEvaluator
{
    public static IncludeExpressionEvaluator Instance { get; } = new();

    public IQueryable<T> GetQuery<T>(IQueryable<T> query,
        ISpecification<T> specification)
        where T : class
    {
        foreach (IncludeExpressionInfo<T> info in specification.IncludeExpressions)
            query = query.Include(info.IncludeExceptions);

        return query;
    }
}