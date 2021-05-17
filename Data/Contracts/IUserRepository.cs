using System.Threading;
using System.Threading.Tasks;
using Entities.Identity;

namespace Data.Contracts
{
    public interface IUserRepository : IRepository<User>
    {
        Task UpdateSecurityStampAsync(User user, CancellationToken cancellationToken);
        Task UpdateLastLoginDateAsync(User user, CancellationToken cancellationToken);
    }
}