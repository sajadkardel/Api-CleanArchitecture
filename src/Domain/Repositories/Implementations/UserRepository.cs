using Domain.Contexts;
using Domain.Entities.Identity;
using Domain.Markers;
using Domain.Repositories.Contracts;

namespace Domain.Repositories.Implementations;

public class UserRepository(ApplicationDbContext dbContext) : Repository<ApplicationUser>(dbContext), IUserRepository, IScopedDependency
{
    public Task UpdateSecurityStampAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        //user.SecurityStamp = Guid.NewGuid();
        return UpdateAsync(user, cancellationToken);
    }
}
