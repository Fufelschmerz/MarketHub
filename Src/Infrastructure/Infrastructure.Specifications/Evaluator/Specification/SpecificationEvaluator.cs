namespace Infrastructure.Specifications.Evaluator.Specification;

using Expressions;
using Expressions.Abstractions;

public class SpecificationEvaluator : ISpecificationEvaluator
{
    private readonly List<IExpressionEvaluator> _evaluators = new();

    public SpecificationEvaluator()
    {
        _evaluators.AddRange(new IExpressionEvaluator[]
        {
            WhereExpressionEvaluator.Instance,
            IncludeExpressionEvaluator.Instance,
            OrderExpressionEvaluator.Instance,
            PaginationExpressionEvaluator.Instance
        });
    }

    public IQueryable<T> BuildQuery<T>(IQueryable<T> query,
        ISpecification<T> specification)
        where T : class
    {
        foreach (IExpressionEvaluator evaluator in _evaluators) query = evaluator.GetQuery(query, specification);

        return query;
    }
}