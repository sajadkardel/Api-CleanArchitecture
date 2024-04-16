using Domain.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Entities.Identity
{
    public class Role : IdentityRole<int>, IEntity<int>
    {
        public string Description { get; set; }
    }

    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable(nameof(Role), typeof(Role).GetParentFolderName());
            builder.Property(p => p.Name).IsRequired().HasMaxLength(32);
            builder.Property(p => p.Description).IsRequired().HasMaxLength(128);
        }
    }
}
