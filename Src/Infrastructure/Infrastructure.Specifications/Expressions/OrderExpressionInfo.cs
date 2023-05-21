namespace Infrastructure.Specifications.Expressions;

using System.Linq.Expressions;
using Enums;

public sealed class OrderExpressionInfo<T>
{
    public OrderExpressionInfo(Expression<Func<T, object>> orderExpression,
        OrderType orderType)
    {
        OrderExpression = orderExpression ?? throw new ArgumentNullException(nameof(orderExpression));
        OrderType = orderType;
    }

    public Expression<Func<T, object>> OrderExpression { get; }

    public OrderType OrderType { get; }
}