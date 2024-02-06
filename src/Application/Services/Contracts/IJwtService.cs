using System.Threading.Tasks;
using Domain;
using Domain.Entities.Identity;

namespace Application.Services.Contracts
{
    public interface IJwtService
    {
        Task<AccessToken> GenerateAsync(User user);
    }
}