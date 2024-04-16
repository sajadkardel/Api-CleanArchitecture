using Domain.Entities;
using Domain.Entities.Identity;
using Domain.Utilities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection;

namespace Domain.Contexts;

public class ApplicationDbContext(DbContextOptions options) : IdentityDbContext<ApplicationUser, ApplicationRole, int>(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var entitiesAssembly = typeof(IEntity).Assembly;

        modelBuilder.RegisterAllEntities<IEntity>(entitiesAssembly);
        modelBuilder.RegisterEntityTypeConfiguration(entitiesAssembly);
        modelBuilder.AddRestrictDeleteBehaviorConvention();
        modelBuilder.AddSequentialGuidForIdConvention();
        modelBuilder.AddPluralizingTableNameConvention();
    }

    public override int SaveChanges()
    {
        BeforeSaveChanges();
        return base.SaveChanges();
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        BeforeSaveChanges();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        BeforeSaveChanges();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        BeforeSaveChanges();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void BeforeSaveChanges()
    {
        var changedEntities = ChangeTracker.Entries();

        foreach (var item in changedEntities)
        {
            if (item.State is EntityState.Added)
            {
                SetAddInfo(item);
                CleanString(item);
            }

            if (item.State is EntityState.Modified)
            {
                SetModifyInfo(item);
                CleanString(item);
            }

            if (item.State is EntityState.Deleted)
            {
                SetDeleteInfo(item);
                ApplySoftDelete(item);
            }

        }
    }

    private void CleanString(EntityEntry item)
    {
        if (item.Entity == null) return;

        var properties = item.Entity.GetType()
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => p.CanRead && p.CanWrite && p.PropertyType == typeof(string));

        foreach (var property in properties)
        {
            var propName = property.Name;
            var val = property.GetValue(item.Entity, null) as string;

            if (string.IsNullOrEmpty(val) is false)
            {
                var newVal = val.Fa2En().FixPersianChars();
                if (newVal == val) continue;
                property.SetValue(item.Entity, newVal, null);
            }
        }
    }

    private void SetAddInfo(EntityEntry item)
    {
        if (item.Entity.GetType().GetProperty("CreatedAt") is not null)
        {
            item.CurrentValues["CreatedAt"] = DateTime.UtcNow;
        }
    }

    private void SetModifyInfo(EntityEntry item)
    {
        if (item.Entity.GetType().GetProperty("ModifiedAt") is not null)
        {
            item.CurrentValues["ModifiedAt"] = DateTime.UtcNow;
        }
    }

    private void SetDeleteInfo(EntityEntry item)
    {
        if (item.Entity.GetType().GetProperty("DeletedAt") is not null)
        {
            item.CurrentValues["DeletedAt"] = DateTime.UtcNow;
        }
    }

    private void ApplySoftDelete(EntityEntry item)
    {
        if (item.Entity.GetType().GetProperty("IsDeleted") is not null)
        {
            item.State = EntityState.Modified;
            item.CurrentValues["IsDeleted"] = true;
        }
    }
}
