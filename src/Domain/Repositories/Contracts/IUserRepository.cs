using Domain.Entities.Identity;

namespace Domain.Repositories.Contracts;

public interface IUserRepository : IRepository<User>
{
    Task UpdateSecurityStampAsync(User user, CancellationToken cancellationToken);
    Task UpdateLastLoginDateAsync(User user, CancellationToken cancellationToken);
}