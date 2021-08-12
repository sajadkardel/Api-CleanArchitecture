using System;
using Common.Enums;
using Entities.Identity;
using WebFramework.Api;

namespace Api.Models.Identity
{
    public class UserSelectDto : BaseDto<UserSelectDto, User>
    {
        public virtual string UserName { get; set; }
        public string FullName { get; set; }
        public virtual string Email { get; set; }
        public virtual string PhoneNumber { get; set; }
        public int Age { get; set; }
        public GenderType Gender { get; set; }
        public bool IsActive { get; set; }
        public DateTimeOffset? LastLoginDate { get; set; }
    }
}
