using Domain.Enums;
using Domain.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Entities.Identity;

public class User : IdentityUser<int>, IEntity<int>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int? Age { get; set; }
    public GenderType? Gender { get; set; }
    public bool IsActive { get; set; }
}

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(nameof(User), typeof(User).GetParentFolderName());
        builder.Property(p => p.UserName).IsRequired().HasMaxLength(32);
        builder.Property(p => p.FirstName).IsRequired().HasMaxLength(32);
        builder.Property(p => p.LastName).IsRequired().HasMaxLength(32);
        builder.Property(p => p.IsActive).HasDefaultValue(true);
    }
}
