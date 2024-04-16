using Domain.Entities.Identity;

namespace Domain.Repositories.Contracts;

public interface IUserRepository : IRepository<ApplicationUser>
{
    Task UpdateSecurityStampAsync(ApplicationUser user, CancellationToken cancellationToken);
}