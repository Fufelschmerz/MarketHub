namespace Infrastructure.Specifications.Evaluator.Expressions;

using Abstractions;
using Enums;
using Exceptions;
using Specifications.Expressions;

public sealed class OrderExpressionEvaluator : IExpressionEvaluator
{
    public static OrderExpressionEvaluator Instance { get; } = new();

    public IQueryable<T> GetQuery<T>(IQueryable<T> query,
        ISpecification<T> specification)
        where T : class
    {
        if (specification.OrderExpressions.Count(x => x.OrderType == OrderType.OrderBy ||
                                                      x.OrderType == OrderType.OrderByDescending) > 1)
            throw new DuplicateOrderChainException();

        IOrderedQueryable<T>? orderedQuery = null;

        foreach (OrderExpressionInfo<T> info in specification.OrderExpressions)
            switch (info.OrderType)
            {
                case OrderType.OrderBy:
                    orderedQuery = query.OrderBy(info.OrderExpression);
                    break;

                case OrderType.OrderByDescending:
                    orderedQuery = query.OrderByDescending(info.OrderExpression);
                    break;

                case OrderType.ThenBy:
                    orderedQuery = orderedQuery?.ThenBy(info.OrderExpression);
                    break;

                case OrderType.ThenByDescending:
                    orderedQuery = orderedQuery?.ThenByDescending(info.OrderExpression);
                    break;
            }

        if (orderedQuery != null)
            query = orderedQuery;

        return query;
    }
}