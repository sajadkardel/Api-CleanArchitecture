using Domain.Entities.Identity;
using Infrastructure.Api;

namespace Api.Models.Identity
{
    public class RoleSelectDto : BaseDto<RoleSelectDto, Role, int>
    {
        public string Name { get; set; }
    }
}
