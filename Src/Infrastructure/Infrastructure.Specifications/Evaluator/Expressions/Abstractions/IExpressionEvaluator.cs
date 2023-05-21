namespace Infrastructure.Specifications.Evaluator.Expressions.Abstractions;

public interface IExpressionEvaluator
{
    IQueryable<T> GetQuery<T>(IQueryable<T> query,
        ISpecification<T> specification)
        where T : class;
}