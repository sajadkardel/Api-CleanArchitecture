using Domain.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Entities.Identity;

public class ApplicationRole : IdentityRole<int>, IEntity<int>
{
    public string Description { get; set; }
}

public class RoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {
        builder.ToTable(nameof(ApplicationRole), typeof(ApplicationRole).GetParentFolderName());
        builder.Property(p => p.Name).IsRequired().HasMaxLength(32);
        builder.Property(p => p.Description).IsRequired().HasMaxLength(128);
    }
}
