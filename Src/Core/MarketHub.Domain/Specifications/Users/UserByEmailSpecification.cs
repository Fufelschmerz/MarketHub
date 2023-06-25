namespace MarketHub.Domain.Specifications.Users;

using Infrastructure.Specifications;
using MarketHub.Domain.Entities.Users;

public class UserByEmailSpecification : Specification<User>
{
    public UserByEmailSpecification(string email)
    {
        Query.Where(x => x.Email == email)
            .Include(x => x.Account)
            .Include(x => x.Roles);
    }
}