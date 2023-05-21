namespace Infrastructure.Specifications.Evaluator.Specification;

public interface ISpecificationEvaluator
{
    IQueryable<T> BuildQuery<T>(IQueryable<T> query,
        ISpecification<T> specification)
        where T : class;
}