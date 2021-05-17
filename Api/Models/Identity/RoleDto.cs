using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Markers;
using Entities.Identity;
using FluentValidation;
using WebFramework.Api;

namespace Api.Models.Identity
{
    public class RoleDto : BaseDto<RoleDto, Role, int>
    {
        public string Name { get; set; }
    }

    public class RoleDtoValidator : AbstractValidator<RoleDto>, IDtoValidator
    {
        public RoleDtoValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("نام مقام را وارد نمایید");
        }
    }
}
