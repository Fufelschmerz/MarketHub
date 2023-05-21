namespace Infrastructure.Specifications.Builders;

using System.Linq.Expressions;
using Enums;
using Expressions;

public sealed class SpecificationBuilder<T> : ISpecificationBuilder<T>
    where T : class
{
    private readonly Specification<T> _specification;

    public SpecificationBuilder(Specification<T> specification)
    {
        _specification = specification ?? throw new ArgumentNullException(nameof(specification));
    }

    public ISpecificationBuilder<T> Skip(int? skip)
    {
        _specification.Skip = skip;

        return this;
    }

    public ISpecificationBuilder<T> Take(int? take)
    {
        _specification.Take = take;

        return this;
    }

    public ISpecificationBuilder<T> Where(Expression<Func<T, bool>> filter)
    {
        WhereExpressionInfo<T> whereExpression = new(filter);

        _specification.AddWhere(whereExpression);

        return this;
    }

    public ISpecificationBuilder<T> Include(Expression<Func<T, object>> include)
    {
        IncludeExpressionInfo<T> includeExpression = new(include);

        _specification.AddInclude(includeExpression);

        return this;
    }

    public ISpecificationBuilder<T> Order(Expression<Func<T, object>> order,
        OrderType orderType)
    {
        OrderExpressionInfo<T> orderExpression = new(order, orderType);

        _specification.AddOrder(orderExpression);

        return this;
    }
}