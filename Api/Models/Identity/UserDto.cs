using Common.Markers;
using Entities.Identity;
using FluentValidation;
using WebFramework.Api;

namespace Api.Models.Identity
{
    public class UserDto : BaseDto<UserDto, User>
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string FullName { get; set; }

        public int Age { get; set; }

        public GenderType Gender { get; set; }
    }

    public class UserDtoValidator : AbstractValidator<UserDto>, IDtoValidator
    {
        public UserDtoValidator()
        {
           
            RuleFor(p => p.UserName).NotEmpty().WithMessage("نام کاربری را وارد نمایید");
            RuleFor(p => p.Email).NotEmpty().WithMessage("ایمیل را وارد نمایید");
            RuleFor(p => p.Password).NotEmpty().WithMessage("کلمه عبور را وارد نمایید");
        }
    }
}
