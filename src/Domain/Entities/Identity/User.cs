using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Common;
using Domain.Enums;
using Domain.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Entities.Identity
{
    public class User : IdentityUser<int>, IEntity<int>
    {
        public User()
        {
            IsActive = true;
        }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }
        public int Age { get; set; }
        public GenderType Gender { get; set; }
        public bool IsActive { get; set; }
        public DateTimeOffset? LastLoginDate { get; set; }
    }

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(User), typeof(User).GetParentFolderName());
            builder.Property(p => p.UserName).IsRequired().HasMaxLength(100);
        }
    }
}
