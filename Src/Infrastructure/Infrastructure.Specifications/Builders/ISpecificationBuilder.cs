namespace Infrastructure.Specifications.Builders;

using System.Linq.Expressions;
using Enums;

public interface ISpecificationBuilder<T>
    where T : class
{
    ISpecificationBuilder<T> Skip(int? skip);

    ISpecificationBuilder<T> Take(int? take);

    ISpecificationBuilder<T> Where(Expression<Func<T, bool>> filter);

    ISpecificationBuilder<T> Include(Expression<Func<T, object>> include);

    ISpecificationBuilder<T> Order(Expression<Func<T, object>> order,
        OrderType orderType);
}