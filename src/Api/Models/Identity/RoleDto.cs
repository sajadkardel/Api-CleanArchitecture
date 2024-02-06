using Domain.Entities.Identity;
using Domain.Markers;
using FluentValidation;
using Infrastructure.Api;

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
