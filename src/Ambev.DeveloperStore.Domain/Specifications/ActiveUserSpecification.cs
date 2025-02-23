using Ambev.DeveloperStore.Domain.Entities;
using Ambev.DeveloperStore.Domain.Enums;

namespace Ambev.DeveloperStore.Domain.Specifications;

public class ActiveUserSpecification : ISpecification<User>
{
    public bool IsSatisfiedBy(User user)
    {
        return user.Status == UserStatus.Active;
    }
}
