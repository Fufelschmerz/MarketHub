namespace Infrastructure.Specifications;

using Builders;
using Expressions;

public abstract class Specification<T> : ISpecification<T>
    where T : class
{
    protected Specification()
    {
        Query = new SpecificationBuilder<T>(this);
    }

    public ISpecificationBuilder<T> Query { get; }

    public List<WhereExpressionInfo<T>> WhereExpressions { get; protected set; } = new();

    public List<IncludeExpressionInfo<T>> IncludeExpressions { get; protected set; } = new();

    public List<OrderExpressionInfo<T>> OrderExpressions { get; protected set; } = new();

    public int? Skip { get; set; }

    public int? Take { get; set; }

    protected internal void AddWhere(WhereExpressionInfo<T> whereExpression)
    {
        if (whereExpression is null)
            throw new ArgumentNullException(nameof(whereExpression));

        WhereExpressions.Add(whereExpression);
    }

    protected internal void AddInclude(IncludeExpressionInfo<T> includeExpression)
    {
        if (includeExpression is null)
            throw new ArgumentNullException(nameof(includeExpression));

        IncludeExpressions.Add(includeExpression);
    }

    protected internal void AddOrder(OrderExpressionInfo<T> orderExpression)
    {
        if (orderExpression is null)
            throw new ArgumentNullException(nameof(orderExpression));

        OrderExpressions.Add(orderExpression);
    }
}