using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Common.Markers;
using Entities.Identity;
using FluentValidation;
using WebFramework.Api;

namespace Api.Models.Identity
{
    public class UserDto : BaseDto<UserDto, User>
    {
        [Required]
        [StringLength(100)]
        public string UserName { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(500)]
        public string Password { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        public int Age { get; set; }

        public GenderType Gender { get; set; }
    }

    public class UserDtoValidator : AbstractValidator<UserDto>, IDtoValidator
    {
        public UserDtoValidator()
        {
            RuleFor(p => p.UserName).NotNull().WithMessage("نام کاربری را وارد نمایید");
        }
    }
}
