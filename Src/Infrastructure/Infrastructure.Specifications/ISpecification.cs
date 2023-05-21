namespace Infrastructure.Specifications;

using Builders;
using Expressions;

public interface ISpecification<T>
    where T : class
{
    ISpecificationBuilder<T> Query { get; }

    List<WhereExpressionInfo<T>> WhereExpressions { get; }

    List<IncludeExpressionInfo<T>> IncludeExpressions { get; }

    List<OrderExpressionInfo<T>> OrderExpressions { get; }

    int? Skip { get; }

    int? Take { get; }
}