namespace MarketHub.Domain.Specifications.Users;

using Entities.Users;
using Infrastructure.Specifications;

public class UserByEmailSpecification : Specification<User>
{
    public UserByEmailSpecification(string name)
    {
        Query.Where(x => x.Name == name);
    }
}