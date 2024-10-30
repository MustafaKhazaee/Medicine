
using Medicine.Domain.Common;
using Medicine.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Medicine.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Medicine.Infrastructure.Persistence;

public class ApplicationDbContext (DbContextOptions options, IHttpContextAccessor httpContextAccessor) : DbContext (options), IApplicationDbContext
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public virtual DbSet<Medication> Medications { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Medication>().HasQueryFilter(m => !m.IsDeleted);

        base.OnModelCreating(modelBuilder);
    }

    public override int SaveChanges()
    {
        var addedEntities = ChangeTracker.Entries<AuditableEntity>().Where(c => c.State == EntityState.Added);

        foreach (var entry in addedEntities)
        {
            entry.Entity.CreationDate = DateTime.UtcNow;
            entry.Entity.CreatedBy = _httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? "Default User";
        }

        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var addedEntities = ChangeTracker.Entries<AuditableEntity>().Where(c => c.State == EntityState.Added);

        foreach (var entry in addedEntities)
        {
            entry.Entity.CreationDate = DateTime.UtcNow;
            entry.Entity.CreatedBy = _httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? "Default User";
        }

        return base.SaveChangesAsync(cancellationToken);
    }
    // Can add for Update . . .
}
