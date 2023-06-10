namespace Infrastructure.Specifications.Evaluator.Expressions;

using Abstractions;

public sealed class PaginationExpressionEvaluator : IExpressionEvaluator
{
    public static PaginationExpressionEvaluator Instance { get; } = new();

    public IQueryable<T> GetQuery<T>(IQueryable<T> query,
        ISpecification<T> specification)
        where T : class
    {
        if (specification.Skip is not null && specification.Skip is not 0)
            query = query.Skip(specification.Skip.Value);

        if (specification.Take is not null)
            query = query.Take(specification.Take.Value);

        return query;
    }
}