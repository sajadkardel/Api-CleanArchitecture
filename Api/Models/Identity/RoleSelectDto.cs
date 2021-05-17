using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Identity;
using WebFramework.Api;

namespace Api.Models.Identity
{
    public class RoleSelectDto : BaseDto<RoleSelectDto, Role, int>
    {
        public string Name { get; set; }
    }
}
